using System;
using System.IO;
using System.Xml;
using Clases;
using static System.Console;

namespace Xml
{
    class Program
    {
        static void Main(string[] args)
        {
            Cabecera("Xml");

            //Se establecen los datos de configuración iniciales.
            DateTime date=DateTime.Now;
            ConsoleColor colorfondo=ConsoleColor.Cyan;
            ConsoleColor colortexto=ConsoleColor.Yellow;
            int alto=30;
            int ancho=90;

     

            //Se generan las clases para cada atributo de la configuración
            NodoFecha Fecha=new NodoFecha("Fecha",date);
            NodoColor ColorFondo=new NodoColor("ColorFondo",colorfondo);
            NodoColor ColorTexto=new NodoColor("ColorTexto",colortexto);
            NodoDimension Alto=new NodoDimension("Alto",alto);
            NodoDimension Ancho=new NodoDimension("Ancho",ancho);
            NodoEspacioLibre EspacioLibre=new NodoEspacioLibre("EspacioLibre");

            //Compruebo si existe el documento Configuracion.xml y si no existe lo genera y añade los datos de configuracion iniciales
            if(!(File.Exists("Configuracion.xml")))
            {
                XmlDocument document=new XmlDocument();
                XmlDeclaration declaration=document.CreateXmlDeclaration("1.0","UTF-8",null);
                document.AppendChild(declaration);
                XmlNode root=document.CreateElement("Configuracion");
                document.AppendChild(root);
                document.Save("Configuracion.xml");

                Fecha.CrearNodo();
                ColorFondo.CrearNodo();
                ColorTexto.CrearNodo();
                Alto.CrearNodo();
                Ancho.CrearNodo();
                EspacioLibre.CrearNodo();

                WriteLine("Se ha generado el documento \"Configuracion.xml\"");

            }
            WriteLine("Ya existe el archivo \"Configuracion.xml\"\n");
            //A continuación pregunta si quiere modificar los datos por pantalla.
            WriteLine("Modifique si quiere los elementos de la configuración");
            Fecha.ModificarNodo();
            ColorFondo.ModificarNodo();
            ColorTexto.ModificarNodo();
            Alto.ModificarNodo();
            Ancho.ModificarNodo();

            //Pido que se introduzcan por pantalla los usuarios
            int orden=0;
            int tipo=0;
            while(orden!=1 && orden!=2)
            { 
                Nuevo:
                Write("Añadir usuario --> 1 \t Salir --> 2\t");
                Casilla();
                if(int.TryParse(ReadLine(),out int num))
                {
                    orden=num;
                }
                WriteLine("\n");

                //Procedimiento para añadir usuarios o salir del programa
                switch(orden)
                {
                    case 1:
                        Write($"Nombre: ");
                        string nombre=ReadLine();
                        Write("Contraseña: ");
                        string contraseña=ReadLine();
                        while(tipo!=1 && tipo!=2)
                        {
                            Write("Administrador --> 1 \t Limitado --> 2 \t");
                            Casilla();
                            if(int.TryParse(ReadLine(),out num))
                            {
                                tipo=num;
                            }
                            WriteLine();
                        }
                        AñadirUsuario usuarionuevo=new AñadirUsuario(nombre,contraseña,tipo);
                        WriteLine("\n");
                        goto Nuevo;
                    case 2:
                        break;
                    default:
                        goto Nuevo;
                }

                WriteLine("El documento \"Configuracion.xml\" contiene:");
                Leer xml=new Leer();
            }
        }
        public static void Cabecera(string nombreprograma)//Esta es la cabecera.
        {
            //Dimensiones de la ventana
            int ancho=90;
            int alto=30;
            SetWindowSize(ancho,alto);

            //Limpio lo que haya antes;
            Clear();
            
            //Dibujo los bordes horizontales
            for(int i=1;i<ancho-1;i++)
            {
                
                
                Write("═");
                SetCursorPosition(i,4);
                Write("═");
                SetCursorPosition(i,0);
                Write("═");
            }
            //Dibujo las esquinas
            SetCursorPosition(0,4);
            WriteLine("╚");
            SetCursorPosition(ancho-1,0);
            WriteLine("╗");
            SetCursorPosition(0,0);
            WriteLine("╔");
            SetCursorPosition(ancho-1,4);
            Write("╝");
            
            //Dibujo los bordes verticales
            for(int i=1;i<4;i++)
            {
                SetCursorPosition(ancho-1,i);
                
                Write("║");
                SetCursorPosition(0,i);
                Write("║");
            }
            
            //Escribo los datos de título del programa, autor, fecha y hora
            ForegroundColor=ConsoleColor.White;
            //Fecha
            SetCursorPosition(1,1);
            DateTime fecha=DateTime.Now;
            WriteLine($"Fecha:{fecha.ToString("dd/MM/yyyy")}");

            //Nombre del programa
            SetCursorPosition(1,2);
            WriteLine($"Nombre del programa: {nombreprograma}");

            //Nombre del autor del programa
            SetCursorPosition(1,3);
            Write("Autor: Víctor Salas Moreno");

            //Hora
            SetCursorPosition(ancho-15,1);
            Write($"Hora: {fecha.ToString("HH:mm:ss")}");

            //Situo el cursor fuera de la caja.
            SetCursorPosition(0,6);
            
            
            ResetColor();
        }
        public static void Casilla()//Marcador donde el usuario responderá por pantalla.
        {
            SetCursorPosition(44,CursorTop-1);
            Write("┌");
            SetCursorPosition(48,CursorTop);
            Write("┐");
            SetCursorPosition(44,CursorTop+2);
            Write("└");
            SetCursorPosition(48,CursorTop);
            Write("┘");
            SetCursorPosition(46,CursorTop-1);
        }
    }
    
}
