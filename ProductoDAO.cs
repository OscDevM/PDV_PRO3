using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

public class ProductoDAO
{
    public int CrearProducto(
        string codigoBarra,
        string nombre,
        string descripcion,
        decimal precio,
        decimal costo,
        int stockInicial,
        int idCategoria,
        string impuestos,
        int usuario,
        string referencia,
        int idInventario)
    {
        try
        {
            using (var con = Conexion.GetConexion())
            {
                con.Open();

                string sql = @"SELECT RegistrarProductoNuevo(
                    @codigo,
                    @nombre,
                    @descripcion,
                    @precio,
                    @costo,
                    @stock,
                    @categoria,
                    @impuestos,
                    @usuario,
                    @referencia,
                    @inventario
                );";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigoBarra);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@costo", costo);
                    cmd.Parameters.AddWithValue("@stock", stockInicial);
                    cmd.Parameters.AddWithValue("@categoria", idCategoria);
                    cmd.Parameters.AddWithValue("@impuestos", impuestos);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@referencia", referencia);
                    cmd.Parameters.AddWithValue("@inventario", idInventario);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al registrar producto: " + ex.Message);
        }
    }

    public DataTable ListarProductos()
    {
        DataTable dt = new DataTable();

        using (var con = Conexion.GetConexion())
        {
            string sql = @"
                        SELECT 
                            p.id_producto,
                            p.codigo_barra,
                            p.nombre,
                            p.descripcion,
                            p.precio,
                            p.costo,
                            p.stock,
                            p.impuestos,
                            c.id_categoria,
                            c.nombre AS categoria,
                            i.id_inventario,
                            i.lugar AS inventario
                        FROM productos p
                        INNER JOIN categoria_producto c 
                            ON p.id_categoria = c.id_categoria
                        INNER JOIN productos_inventario pi 
                            ON p.id_producto = pi.id_producto
                        INNER JOIN inventario i 
                            ON pi.id_inventario = i.id_inventario
                        WHERE p.activo = TRUE
                        ORDER BY p.nombre;";

            using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con))
            {
                da.Fill(dt);
            }
        }

        return dt;
    }

    public void AgregarStock(int idProducto, int cantidad, string referencia, int usuario)
    {
        try
        {
            using (var con = Conexion.GetConexion())
            {
                con.Open();

                string sql = @"SELECT EntradaProductoExistente(
                    @producto,
                    @cantidad,
                    @referencia,
                    @usuario
                );";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@producto", idProducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@referencia", referencia);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al agregar stock: " + ex.Message);
        }
    }

    public void EliminarProducto(int idProducto)
    {
        try
        {
            using (var con = Conexion.GetConexion())
            {
                con.Open();

                string sql = @"UPDATE productos 
                               SET activo = FALSE 
                               WHERE id_producto = @id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al eliminar producto: " + ex.Message);
        }
    }
    public DataTable ListarCategorias()
    {
        DataTable dt = new DataTable();

        using (var con = Conexion.GetConexion())
        {
            string sql = "SELECT id_categoria, nombre FROM categoria_producto WHERE activo = TRUE";
            using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }
    public DataTable ListarInventarios()
    {
        DataTable dt = new DataTable();

        using (var con = Conexion.GetConexion())
        {
            string sql = "SELECT id_inventario, lugar FROM inventario";
            using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }
}
