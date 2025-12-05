using System.ComponentModel;
using ModelContextProtocol.Server;

public class EmpaparNumeros
{
    [McpServerTool, Description("Realiza una operacion para empapar dos números y obtiene el resultado de esta operación 3a+b^2")]
    public double MetodoEmpaparNumeros(
        [Description("primer número a empapar.")] double aNumero, 
        [Description("segundo número a empapar.")]double bNumero) 
    {
        var resultado = 3 * aNumero + Math.Pow(bNumero,2);
        return resultado;
    }
}