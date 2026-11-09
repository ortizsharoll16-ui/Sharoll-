using System;
using System.Text;
using System.Collections.Generic;

class HabitacionHotel
{
    private int numero;
    private string tipo;
    private double precioPorNoche;
    private bool disponible;
    private string clienteActual;

    public int Numero
    {
        get { return numero; }
    }

    public string Tipo
    {
        get { return tipo; }
        set
        {
            if (value == "Individual" || value == "Doble" || value == "Suite")
            {
                tipo = value;
            }
            else
            {
                Console.WriteLine("Tipo de habitación no válido.");
            }
        }
    }

    public double PrecioPorNoche
    {
        get { return precioPorNoche; }
        set
        {
            if (value > 0)
            {
                precioPorNoche = value;
            }
            else
            {
                Console.WriteLine("El precio debe ser mayor que cero.");
            }
        }
    }

    public bool Disponible
    {
        get { return disponible; }
    }

    public string ClienteActual
    {
        get { return clienteActual; }
    }

    public HabitacionHotel(int numero, string tipo, double precioPorNoche)
    {
        this.numero = numero;
        Tipo = tipo;
        PrecioPorNoche = precioPorNoche;
        disponible = true;
        clienteActual = "";
    }

    public double Reservar(string nombreCliente, int noches)
    {
        if (disponible && !string.IsNullOrWhiteSpace(nombreCliente) && noches > 0)
        {
            disponible = false;
            clienteActual = nombreCliente;
            return precioPorNoche * noches;
        }

        return -1;
    }

    public bool Liberar()
    {
        if (!disponible)
        {
            disponible = true;
            clienteActual = "";
            return true;
        }

        return false;
    }

    public string MostrarInfo()
    {
        string estado = disponible ? "Disponible" : "Ocupada";

        string info = "Habitación: " + numero +
                      "\nTipo: " + tipo +
                      "\nPrecio por noche: $" + precioPorNoche.ToString("N0") +
                      "\nEstado: " + estado;

        if (!disponible)
        {
            info += "\nCliente actual: " + clienteActual;
        }

        return info;
    }

    // A. APLICAR DESCUENTO CON VALIDACIÓN

    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje > 0 && porcentaje <= 100)
        {
            double descuento = precioPorNoche * porcentaje / 100.0;
            double nuevoPrecio = precioPorNoche - descuento;

            if (nuevoPrecio > 0)
            {
                precioPorNoche = nuevoPrecio;
                Console.WriteLine("Descuento aplicado correctamente.");
            }
            else
            {
                Console.WriteLine("El descuento no puede dejar el precio en cero.");
            }
        }
        else
        {
            Console.WriteLine("Porcentaje de descuento no válido.");
        }
    }

    // B. OBTENER DATOS RESUMIDOS

    public string ObtenerDatosResumen()
    {
        string estado = disponible ? "Libre" : "Ocupada por: " + clienteActual;

        return "[" + numero + "] " + tipo +
               " - $" + precioPorNoche.ToString("N0") +
               " - " + estado;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // CREACIÓN DE LAS HABITACIONES

        HabitacionHotel habitacion101 =
            new HabitacionHotel(101, "Individual", 50000);

        HabitacionHotel habitacion202 =
            new HabitacionHotel(202, "Doble", 80000);

        HabitacionHotel habitacion303 =
            new HabitacionHotel(303, "Suite", 150000);

        // LISTA DE HABITACIONES

        List<HabitacionHotel> listaHabitaciones =
            new List<HabitacionHotel>();

        listaHabitaciones.Add(habitacion101);
        listaHabitaciones.Add(habitacion202);
        listaHabitaciones.Add(habitacion303);

        int opcion;

        // MENÚ PRINCIPAL

        do
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine(" HOTEL NOMADAS DEL CARIBE");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Mostrar habitaciones");
            Console.WriteLine("2. Reservar habitación");
            Console.WriteLine("3. Liberar habitación");
            Console.WriteLine("4. Aplicar descuento");
            Console.WriteLine("5. Mostrar resumen");
            Console.WriteLine("6. Mostrar métricas");
            Console.WriteLine("0. Salir");
            Console.WriteLine("==================================");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = -1;
            }

            switch (opcion)
            {
                // OPCIÓN 1: MOSTRAR HABITACIONES

                case 1:

                    Console.WriteLine("\n===== LISTA DE HABITACIONES =====");

                    foreach (HabitacionHotel h in listaHabitaciones)
                    {
                        Console.WriteLine(h.MostrarInfo());
                        Console.WriteLine();
                    }

                    break;

                // OPCIÓN 2: RESERVAR HABITACIÓN

                case 2:

                    Console.Write("\nIngrese el número de habitación: ");
                    int numeroReserva;

                    if (!int.TryParse(Console.ReadLine(), out numeroReserva))
                    {
                        Console.WriteLine("Número no válido.");
                        break;
                    }

                    HabitacionHotel habitacionReserva =
                        listaHabitaciones.Find(h => h.Numero == numeroReserva);

                    if (habitacionReserva == null)
                    {
                        Console.WriteLine("La habitación no existe.");
                        break;
                    }

                    Console.Write("Ingrese el nombre del cliente: ");
                    string nombreCliente = Console.ReadLine();

                    Console.Write("Ingrese la cantidad de noches: ");
                    int noches;

                    if (!int.TryParse(Console.ReadLine(), out noches))
                    {
                        Console.WriteLine("Cantidad de noches no válida.");
                        break;
                    }

                    double costo = habitacionReserva.Reservar(nombreCliente, noches);

                    if (costo != -1)
                    {
                        Console.WriteLine("Reserva realizada correctamente.");
                        Console.WriteLine("Costo total: $" + costo.ToString("N0"));
                    }
                    else
                    {
                        Console.WriteLine("No se pudo realizar la reserva.");
                    }

                    break;

                // OPCIÓN 3: LIBERAR HABITACIÓN

                case 3:

                    Console.Write("\nIngrese el número de habitación: ");
                    int numeroLiberar;

                    if (!int.TryParse(Console.ReadLine(), out numeroLiberar))
                    {
                        Console.WriteLine("Número no válido.");
                        break;
                    }

                    HabitacionHotel habitacionLiberar =
                        listaHabitaciones.Find(h => h.Numero == numeroLiberar);

                    if (habitacionLiberar == null)
                    {
                        Console.WriteLine("La habitación no existe.");
                    }
                    else if (habitacionLiberar.Liberar())
                    {
                        Console.WriteLine("Habitación liberada correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("La habitación ya estaba disponible.");
                    }

                    break;

                // OPCIÓN 4: APLICAR DESCUENTO

                case 4:

                    Console.Write("\nIngrese el número de habitación: ");
                    int numeroDescuento;

                    if (!int.TryParse(Console.ReadLine(), out numeroDescuento))
                    {
                        Console.WriteLine("Número no válido.");
                        break;
                    }

                    HabitacionHotel habitacionDescuento =
                        listaHabitaciones.Find(h => h.Numero == numeroDescuento);

                    if (habitacionDescuento == null)
                    {
                        Console.WriteLine("La habitación no existe.");
                        break;
                    }

                    Console.Write("Ingrese el porcentaje de descuento: ");
                    int porcentaje;

                    if (!int.TryParse(Console.ReadLine(), out porcentaje))
                    {
                        Console.WriteLine("Porcentaje no válido.");
                        break;
                    }

                    habitacionDescuento.AplicarDescuento(porcentaje);

                    Console.WriteLine("Precio actual: $" +
                        habitacionDescuento.PrecioPorNoche.ToString("N0"));

                    break;

                // OPCIÓN 5: MOSTRAR RESUMEN

                case 5:

                    Console.WriteLine("\n===== RESUMEN DE HABITACIONES =====");

                    foreach (HabitacionHotel h in listaHabitaciones)
                    {
                        Console.WriteLine(h.ObtenerDatosResumen());
                    }

                    break;

                // OPCIÓN 6: MOSTRAR MÉTRICAS

                case 6:

                    MostrarMetricas(listaHabitaciones);

                    break;

                // OPCIÓN 0: SALIR

                case 0:

                    Console.WriteLine("\nSaliendo del sistema...");

                    break;

                default:

                    Console.WriteLine("Opción no válida.");

                    break;
            }

        } while (opcion != 0);

        // ESTADO FINAL

        Console.WriteLine("\n===== ESTADO FINAL =====");

        foreach (HabitacionHotel h in listaHabitaciones)
        {
            Console.WriteLine(h.ObtenerDatosResumen());
        }

        // MOSTRAR MÉTRICAS FINALES

        MostrarMetricas(listaHabitaciones);

        Console.WriteLine("\nGracias por utilizar Hotel Nómadas del Caribe.");
    }

    // MÉTODO PARA CALCULAR LAS MÉTRICAS

    static void MostrarMetricas(List<HabitacionHotel> listaHabitaciones)
    {
        int libres = 0;
        int ocupadas = 0;
        double sumaPrecios = 0;

        foreach (HabitacionHotel h in listaHabitaciones)
        {
            if (h.Disponible)
            {
                libres++;
            }
            else
            {
                ocupadas++;
            }

            sumaPrecios += h.PrecioPorNoche;
        }

        double promedio = 0;

        if (listaHabitaciones.Count > 0)
        {
            promedio = sumaPrecios / listaHabitaciones.Count;
        }

        Console.WriteLine("\n===== MÉTRICAS FINALES =====");

        Console.WriteLine("Habitaciones libres: " + libres);
        Console.WriteLine("Habitaciones ocupadas: " + ocupadas);
        Console.WriteLine("Total de habitaciones: " + listaHabitaciones.Count);

        Console.WriteLine("Precio promedio por noche: $" +
                          promedio.ToString("N0"));
    }
}
