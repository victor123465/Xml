using System;
using System.Xml;
using System.IO;
using static System.Console;
using System.Threading;


namespace Clases
{
    //Interface que se va a usar para los nodos de configuracion
    interface IinfoNodo
    {
        void CrearNodo();
        void ModificarNodo();
    }
   
    //En esta clase se establece la fecha en el archivo de configuración
    public class NodoFecha:IinfoNodo
    {
        public XmlDocument document=new XmlDocument();
        public string NombreNodo;
        public DateTime Fechas;
        public NodoFecha(string nombre,DateTime fecha)//Constructor
        {
            this.NombreNodo=nombre;
            this.Fechas=fecha;
        }
        public void CrearNodo()//Añade el nodo fecha al documento xml
        {
            document.Load("Configuracion.xml");
            XmlNode root=document.DocumentElement;
            XmlNode nodo=document.CreateElement(this.NombreNodo);
            nodo.InnerText=this.Fechas.ToString("dd/MM/yyyy");
            root.AppendChild(nodo);
            document.Save("Configuracion.xml");
        }
        public void ModificarNodo()//Modifica el nodo por pantalla
        {
            string entrada=null;
            WriteLine($"{this.NombreNodo}: {this.Fechas.ToString("dd/MM/yyyy")}");
            while(entrada!="s" && entrada!="n")
            {
                Write($"¿Modificar {this.NombreNodo} ?\tSí-->s\tNo-->n");
                Casilla casilla=new Casilla();
                entrada=ReadLine();
                WriteLine();
            }
            if(entrada=="s")
            {
                re:
                Write("Fecha: ");
                try{
                    this.Fechas=DateTime.Parse(ReadLine());
                }catch
                {
                    WriteLine("Fecha incorrecta.");
                    goto re;
                }
                
                document.Load("Configuracion.xml");
                XmlNode root=document.DocumentElement;
                XmlNode nod=root.SelectSingleNode(this.NombreNodo);
                nod.InnerText=this.Fechas.ToString("dd/MM/yyyy");
                document.Save("Configuracion.xml");
            }
            WriteLine("\n");
        }
        
    }
    //Nodo que establece tanto el color de fondo como el color del texto
    public class NodoColor:IinfoNodo
    {
        public string NombreNodo;
        public ConsoleColor Color;
        public XmlDocument document=new XmlDocument();
        public NodoColor(string nombre,ConsoleColor color)
        {
            this.NombreNodo=nombre;
            this.Color=color;
        }
        public void CrearNodo()
        {
            document.Load("Configuracion.xml");
            XmlNode root=document.DocumentElement;
            XmlNode nodo=document.CreateElement(this.NombreNodo);
            nodo.InnerText=this.Color.ToString();
            root.AppendChild(nodo);
            document.Save("Configuracion.xml");
        }
        public void ModificarNodo()
        {
            string entrada=null;
            WriteLine($"{this.NombreNodo}: {this.Color}");
            while(entrada!="s" && entrada!="n")
            {
                Write($"¿Modificar {this.NombreNodo} ?\tSí-->s\tNo-->n");
                Casilla casilla=new Casilla();
                entrada=ReadLine();
                WriteLine();
            }
            if(entrada=="s")
            {
                document.Load("Configuracion.xml");
                XmlNode root=document.DocumentElement;
                XmlNode nodo=root.SelectSingleNode(this.NombreNodo);
                ConsoleColor[] colors = (ConsoleColor[]) ConsoleColor.GetValues(typeof(ConsoleColor));

                WriteLine("Color:");
                int i=0;
                int num=-1;
                foreach(var color in colors)
                {
                    CursorLeft=25;
                    WriteLine($"{color}-->{i++}");
                }
                while(num<0 || num>15)
                {
                    re:
                    Write("Nuevo color: ");
                    try{
                        num=int.Parse(ReadLine());
                    }catch
                    {
                        goto re;
                    }
                }
                ConsoleColor color1=colors[num];
                nodo.InnerText=$"{color1}";
                document.Save("Configuracion.xml");
            }
            WriteLine("\n");
        }
        
    }
    //Nodo que establece las dimensiones de la ventana
    public class NodoDimension:IinfoNodo
    {
        public string NombreNodo;
        public int Dimension;
        public XmlDocument document=new XmlDocument();
        public NodoDimension(string nombre,int dimension)
        {
            this.NombreNodo=nombre;
            this.Dimension=dimension;
        }
        public void CrearNodo()
        {
            document.Load("Configuracion.xml");
            XmlNode root=document.DocumentElement;
            XmlNode nodo=document.CreateElement(this.NombreNodo);
            nodo.InnerText=this.Dimension.ToString();
            root.AppendChild(nodo);
            document.Save("Configuracion.xml");
        }
        public void ModificarNodo()
        {
            string entrada=null;
            WriteLine($"{this.NombreNodo}: {this.Dimension}");
            while(entrada!="s" && entrada!="n")
            {
                Write($"¿Modificar {this.NombreNodo} ?\tSí-->s\tNo-->n");
                Casilla casilla=new Casilla();
                entrada=ReadLine();
                WriteLine();
            }
            if(entrada=="s")
            {
                document.Load("Configuracion.xml");
                XmlNode root=document.DocumentElement;
                XmlNode nodo=root.SelectSingleNode(this.NombreNodo);
                int alto=0;
                re:
                Write($"{this.NombreNodo}: ");
                try{
                    alto=int.Parse(ReadLine());
                }catch
                {
                    goto re;
                }
                nodo.InnerText=$"{alto}";
                document.Save("Configuracion.xml");
            }
            WriteLine("\n");
        }
    }
    //En esta clase se obtiene el espacio libre en el disco duro
    public class NodoEspacioLibre:IinfoNodo
    {
        public string NombreNodo;
        public XmlDocument document=new XmlDocument();
        public NodoEspacioLibre(string nombre)
        {
            this.NombreNodo=nombre;
        }
        public void CrearNodo()
        {
            document.Load("Configuracion.xml");
            XmlNode root=document.DocumentElement;
            DriveInfo[] allDrivers=DriveInfo.GetDrives();
            foreach(DriveInfo d in allDrivers)
            {
                if(d.Name=="C:\\")
                {
                    XmlNode esplibre=document.CreateElement("EspacioLibre");
                    esplibre.InnerText=$"{d.AvailableFreeSpace}";
                    root.AppendChild(esplibre);
                }
            }
            document.Save("Configuracion.xml");
        }
        public void ModificarNodo()
        {     
        }
    }
    public class Usuario//Clase para generar los usuarios con nombre, contraseña y tipo de usuario
    {
       public string Nombre;
        public string Contraseña;
        public string Tipo;
        public Usuario(string nombre,string contraseña,int tipo)
        {
            this.Nombre=nombre;
            this.Contraseña=contraseña;
            if(tipo==1)
            {
                this.Tipo="Administrador";
            }else if(tipo==2)
            {
                this.Tipo="Limitado";
            }
        }
        
    }
    //En la clase AñadirUsuario se hereda las propiedades de Usuario y se añaden al documento
    public class AñadirUsuario:Usuario
    {
        public AñadirUsuario(string nombre,string contraseña,int tipo):base(nombre,contraseña,tipo)
        {
            XmlDocument doc =new XmlDocument(); //Nuevo documento xml

            doc.Load("Configuracion.xml");    //Cargo el documento
            XmlNode root=doc.DocumentElement; //Cargo el nodo principal Configuracion
            XmlNode node=doc.CreateElement("Usuario");   //Genero el nodo correspondiente al usuario

            XmlNode tipox=doc.CreateElement("Tipo");
            tipox.InnerText=this.Tipo;

            XmlNode nombrex=doc.CreateElement("Nombre"); //Creo el nodo Nombre
            nombrex.InnerText=this.Nombre; //Establezco el nombre del usuario

            XmlNode contraseñax=doc.CreateElement("Contraseña");//Creo el nodo contraseña
            contraseñax.InnerText=this.Contraseña; //Establezco la contraseña

            node.AppendChild(tipox);
            node.AppendChild(nombrex); //Añado el nombre al tipo de usuario
            node.AppendChild(contraseñax); //Añado la contraseña al tipo de usuario
            root.AppendChild(node);//Añado el tipo de usuario a la configuracion

            doc.Save("Configuracion.xml");  //Guardo el archivo Configuracion.xml
        }
    }
     public class Leer //Clase para leer el documento xml y mostrarlo por consola
    {
        public Leer()
        {
            XmlDocument document=new XmlDocument();
            document.Load("Configuracion.xml");
            XmlNode root=document.DocumentElement;
            XmlNodeList lista=root.ChildNodes;

            foreach(XmlNode nodo in lista)
            {
                ForegroundColor=ConsoleColor.Blue;
                WriteLine($"<{nodo.Name}>");
                foreach(XmlNode nod in nodo.ChildNodes)
                {
                    ForegroundColor=ConsoleColor.Green;
                    Write($"\t<{nod.Name}>");
                    ForegroundColor=ConsoleColor.White;
                    Write($"{nod.InnerText}");
                    ForegroundColor=ConsoleColor.Green;
                    WriteLine($"</{nod.Name}>");
                }
                ForegroundColor=ConsoleColor.Blue;
                WriteLine($"</{nodo.Name}>");
            }
            ForegroundColor=ConsoleColor.Gray;
        }
    }
    public class Casilla//Clase que establece una casilla donde el usuario responde por pantalla.
    {
        public Casilla()
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
