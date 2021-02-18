using System;
using Dapper;
using Dapper.Contrib;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper.Contrib.Extensions;
using System.Collections;
using System.Collections.Generic;


namespace pai_natal


{
    [Dapper.Contrib.Extensions.Table("Crianca")]
    public class crianca
    {
        public string Nome { get; set; }
        public int Comportamento { get; set; }
        public string Presente { get; set; }
        public int Idade { get; set; }
    }

    public class presente
    {
        public int Id_presente{ get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }


    class Program

    {
        List<presente> Pre = new List<presente>();


        static void Main(string[] args)
        {


            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }


        }



        private static bool MainMenu()

        {
            string db = @"Server=localhost;Database=natal;Uid=root;Pwd=";

            Console.Clear();
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1- Adicionar Presentes");
            Console.WriteLine("2- Adicionar Crianças");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelecione uma opção: ");
            int option = Int32.Parse(Console.ReadLine());

            
            switch (option)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("ID- NOME - Quantidade");
                        using (MySqlConnection connection = new MySqlConnection(db))
                        {
                            var myEvent = connection.Query<presente>("SELECT * from Presentes");
                           // Console.WriteLine(myEvent.ID + " - " + myEvent.Nome + " - " + myEvent.Quantidade);
                            foreach (var myEventlist in myEvent)
                            {
                                Console.WriteLine(myEventlist.Id_presente+ " - " + myEventlist.Nome + " - " + myEventlist.Quantidade);

                            }


                        }


                        Console.Write("Nome Presente:");
                        var NomePresente = Console.ReadLine();
                        Console.Write("Quantidade:");
                        var QuantidadePresente = Int32.Parse(Console.ReadLine());

                        using (MySqlConnection connection = new MySqlConnection(db))
                        {
                            var newEvent = new presente
                            {
                                Nome = NomePresente,
                                Quantidade = QuantidadePresente

                            };
                            connection.Insert(newEvent);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }

                    return true;


                case 2:


                    try
                    {



                        Console.Write("Nome:");
                        var rNome = Console.ReadLine();
                        Console.Write("Comportamento 1 - Mau / 2 - Bom :");
                        var rComportamento = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Presentes Disponiveis:");
                        try
                        {
                            Console.WriteLine("NOME - Quantidade");
                            using (MySqlConnection connection = new MySqlConnection(db))
                            {
                                var myEvent = connection.Query<presente>("SELECT  * From Presentes");

                       



                                foreach (var myEventlist in myEvent)
                                {
                                    Console.WriteLine(myEventlist.Id_presente + " - " + myEventlist.Nome + " - " + myEventlist.Quantidade);
                                }


                            }
                        
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("{0} Exception caught.", e);
                        }

                        Console.Write("ID Do Presente Escolhido :");
                        var rPresente = Console.ReadLine();
                        var query = $"SELECT Id_presente , nome , quantidade FROM presentes WHERE id_presente = {rPresente}";
                        try
                        {
                            using (MySqlConnection connection = new MySqlConnection(db))

                            {
                             
                                var myEvent = connection.QueryFirst<presente>(query);
                                var Qt = myEvent.Quantidade - 1;
                                var Idp = myEvent.Id_presente;
                                var q2 = $"UPDATE presentes SET quantidade = {Qt} WHERE id_presente = {Idp}";
                                connection.Execute(q2);
                            }

                        }
                          catch (Exception e)
                        {
                            Console.WriteLine("{0} Exception caught.", e);
                        }
                    

                        
                        Console.Write("Idade:");
                        var rIdade = Int32.Parse(Console.ReadLine());


                        using (MySqlConnection connection = new MySqlConnection(db))
                        {
                            var newEvent = new crianca
                            {
                                Nome = rNome,
                                Comportamento = rComportamento,
                                Presente = rPresente,
                                Idade = rIdade
                            };
                            connection.Insert(newEvent);
                        }



                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }



                    return true;

                case 3: return false;
                default:
                    return true;

            }

           
        }

    }
}
