using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    class Fruta
    {
        public string Nombre { get; set; }
        public string Color { get; set; }

        public Fruta(string nombre, string color)
        {
            Nombre = nombre;
            Color = color;
        }

        public override string ToString()
        {
            return $"{Nombre} ({Color})";
        }
    }

    static void Main()
    {
        string filePath = "frutas.txt";
        List<Fruta> frutas = new List<Fruta>();

        // Cargar frutas desde el archivo al iniciar el programa
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 2)
                {
                    frutas.Add(new Fruta(parts[0], parts[1]));
                }
            }
        }

        int opcion;

        do
        {
            Console.Clear(); // Limpia la consola antes de mostrar el menú

            Console.WriteLine("Menu de Frutas");
            Console.WriteLine("1. Agregar fruta");
            Console.WriteLine("2. Eliminar fruta");
            Console.WriteLine("3. Mostrar todas las frutas");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el nombre de la fruta a agregar: ");
                    string frutaAgregar = Console.ReadLine();
                    Console.Write("Ingrese el color de la fruta: ");
                    string colorFruta = Console.ReadLine();
                    frutas.Add(new Fruta(frutaAgregar, colorFruta));
                    Console.WriteLine($"{frutaAgregar} de color {colorFruta} ha sido agregada a la lista.");
                    GuardarFrutasEnArchivo(frutas, filePath); // Guardar cambios en el archivo
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey(); // Pausa para que el usuario vea el mensaje
                    break;
                case 2:
                    Console.Write("Ingrese el nombre de la fruta a eliminar: ");
                    string frutaEliminar = Console.ReadLine();
                    bool frutaEliminada = false;

                    for (int i = 0; i < frutas.Count; i++)
                    {
                        if (frutas[i].Nombre.Equals(frutaEliminar, StringComparison.OrdinalIgnoreCase))
                        {
                            frutas.RemoveAt(i);
                            frutaEliminada = true;
                            break;
                        }
                    }

                    if (frutaEliminada)
                    {
                        Console.WriteLine($"{frutaEliminar} ha sido eliminada de la lista.");
                        GuardarFrutasEnArchivo(frutas, filePath); // Guardar cambios en el archivo
                    }
                    else
                    {
                        Console.WriteLine($"{frutaEliminar} no se encontró en la lista.");
                    }
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey(); // Pausa para que el usuario vea el mensaje
                    break;
                case 3:
                    Console.WriteLine("Lista de frutas:");
                    foreach (Fruta fruta in frutas)
                    {
                        Console.WriteLine(fruta);
                    }
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey(); // Pausa para que el usuario vea la lista
                    break;
                case 4:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey(); // Pausa para que el usuario vea el mensaje de error
                    break;
            }
        } while (opcion != 4);
    }

    static void GuardarFrutasEnArchivo(List<Fruta> frutas, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Fruta fruta in frutas)
            {
                writer.WriteLine($"{fruta.Nombre};{fruta.Color}");
            }
        }
    }
}
