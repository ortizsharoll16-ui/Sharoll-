using System;
using System.Text;

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


    

    
    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje >= 0 && porcentaje <= 100)
        {
            precioPorNoche = precioPorNoche -
                             (precioPorNoche * porcentaje / 100.0);
        }
        else
        {
            Console.WriteLine("Porcentaje de descuento no válido.");
        }
    }
}






class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

       
        HabitacionHotel habitacion101 =
            new HabitacionHotel(101, "Individual", 50000);

        HabitacionHotel habitacion202 =
            new HabitacionHotel(202, "Doble", 80000);

        HabitacionHotel habitacion303 =
            new HabitacionHotel(303, "Suite", 150000);


        
       
        Console.WriteLine("===== ESTADO INICIAL =====\n");

        Console.WriteLine(habitacion101.MostrarInfo());
        Console.WriteLine();

        Console.WriteLine(habitacion202.MostrarInfo());
        Console.WriteLine();

        Console.WriteLine(habitacion303.MostrarInfo());
        Console.WriteLine();

        

        
        
        Console.WriteLine("===== OPERACIONES =====\n");


        
        double costo = habitacion101.Reservar("Anyel Torregroza", 3);

        if (costo != -1)
        {
            Console.WriteLine("Reserva de Anyel Torregroza realizada.");
            Console.WriteLine("Costo total: $" + costo.ToString("N0"));
        }
        else
        {
            Console.WriteLine("No se pudo realizar la reserva.");
        }

        Console.WriteLine();

        

        
        costo = habitacion202.Reservar("Humberto", 2);

        if (costo != -1)
        {
            Console.WriteLine("Reserva de Humberto Martinez realizada.");
            Console.WriteLine("Costo total: $" + costo.ToString("N0"));
        }
        else
        {
            Console.WriteLine("No se pudo realizar la reserva.");
        }

        Console.WriteLine();

 
        costo = habitacion101.Reservar("Otro Cliente", 2);

        if (costo == -1)
        {
            Console.WriteLine("Reserva rechazada: la habitación 101 está ocupada.");
        }
        else
        {
            Console.WriteLine("Reserva realizada. Costo: $" + costo.ToString("N0"));
        }

        Console.WriteLine();



      
        if (habitacion101.Liberar())
        {
            Console.WriteLine("Habitación 101 liberada correctamente.");
        }
        else
        {
            Console.WriteLine("La habitación 101 ya estaba disponible.");
        }

        Console.WriteLine();


        

        habitacion303.AplicarDescuento(10);

        Console.WriteLine("Descuento del 10% aplicado a la habitación 303.");
        Console.WriteLine("Nuevo precio por noche: $" +
                          habitacion303.PrecioPorNoche.ToString("N0"));

        Console.WriteLine();


      
        costo = habitacion101.Reservar("Carlos Ruiz", 1);

        if (costo != -1)
        {
            Console.WriteLine("Reserva de Carlos Ruiz realizada.");
            Console.WriteLine("Costo total: $" + costo.ToString("N0"));
        }
        else
        {
            Console.WriteLine("No se pudo realizar la reserva.");
        }



      
        Console.WriteLine("\n===== ESTADO FINAL =====\n");

        Console.WriteLine(habitacion101.MostrarInfo());
        Console.WriteLine();

        Console.WriteLine(habitacion202.MostrarInfo());
        Console.WriteLine();

        Console.WriteLine(habitacion303.MostrarInfo());
        Console.WriteLine();



        
        int ocupadas = 0;

        if (!habitacion101.Disponible)
        {
            ocupadas++;
        }

        if (!habitacion202.Disponible)
        {
            ocupadas++;
        }

        if (!habitacion303.Disponible)
        {
            ocupadas++;
        }

        Console.WriteLine("Total de habitaciones ocupadas: " + ocupadas);
    }
}
