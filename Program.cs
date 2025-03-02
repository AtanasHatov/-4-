using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1
{
    class Program
    {
        static string StringConnection = @"Data Source=DESKTOP-6VMBKCR\SQLEXPRESS01;Initial Catalog=CinemaCenter;Integrated Security=True";
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1 - Добави Studio");
                Console.WriteLine("2 - Добави Director");
                Console.WriteLine("3 - Добави Film");
                Console.WriteLine("4 - Добави Actor");
                Console.WriteLine("5 - Свързване на Films and Actors");
                Console.WriteLine("6 - Изход");
                Console.WriteLine("7 - Изведи цялата информация от таблица Actors");
                Console.WriteLine("8 - Имената на режисьора на даден филм по неговото име");
                Console.WriteLine("9 - Заглавието на филмите, на които режисьорите имат повече от 11 години трудов стаж");
                Console.WriteLine("10 - Заглавието на филма, на най-опитния режисьор");
                Console.WriteLine("11 - Заглавието на филма, име и фамилия на актьора, който участва във филма, име и фамилия на режисьора на филма, АКО актьора има повече от 3 години трудов стаж");
                Console.WriteLine("12 - Име и фамилия на актьора, който има повече от (въведи години) години трудов стаж");
                Console.WriteLine("13 - Име и фамилия на режисьора, който има (въведи години) години трудов стаж");
                Console.WriteLine("14 - Изведи цялата информация от таблица Studios");
                Console.WriteLine("15 - Изведи цялата информация от таблица Directors");
                Console.WriteLine("16 - Изведи цялата информация от таблица Films");

                int n = int.Parse(Console.ReadLine());

                switch (n)
                {
                    case 1: AddStudios();
                        break;
                    case 2:
                        AddDirectors();
                        break;
                    case 3:
                        AddFilms();
                        break;
                    case 4:
                        AddActors();
                        break;
                    case 5:
                        AddFilmsActors();
                        break;
                    case 6:
                        Console.WriteLine();
                        break;
                    case 7:
                        Zaqvka1();
                        break;
                    case 8:
                        Zaqvka2();
                        break;
                    case 9:
                        Zaqvka3();
                        break;
                    case 10:
                        Zaqvka4();
                        break;
                    case 11:
                        Zaqvka5();
                        break;
                    case 12:
                        Zaqvka6();
                        break;
                    case 13:
                        Zaqvka7();
                        break;
                    case 14:
                        Zaqvka8();
                        break;
                    case 15:
                        Zaqvka9();
                        break;
                    case 16:
                        Zaqvka10();
                        break;
                    default:
                        Console.WriteLine("Няма такава опция в менюто!!!!!");
                        break;
                }

                if (n == 6)
                {
                    break;
                }
            }
        }

        static void AddStudios()
        {
            Console.WriteLine("Въведете име на студиото:");
            string nameStudio = Console.ReadLine();

            Console.WriteLine("Въведете адрес на студиото:");
            string address = Console.ReadLine();

            Console.WriteLine("Въведете телефон за контакт на студиото:");
            string contact = Console.ReadLine();

            Console.WriteLine("Въведете имейл на студиото:");
            string email = Console.ReadLine();

            string zaqvka = "INSERT INTO Studios([name], [address], [contact], [email]) VALUES (@Name, @Address, @Contact, @Email)";
            using (SqlConnection conn = new SqlConnection(StringConnection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(zaqvka, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@Name", nameStudio);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Contact", contact);
                        cmd.Parameters.AddWithValue("@Email", email);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Успешно добавихте запис в Studios");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void AddDirectors()
        {
            Console.WriteLine("Въведете име на режисьора:");
            string firstname = Console.ReadLine();

            Console.WriteLine("Въведете фамилия на режисьора:");
            string surnamename = Console.ReadLine();

            Console.WriteLine("Въведете трудов стаж (в години) на режисьора:");
            int yearswork = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете телефон за контакт на режисьора:");
            string phone = Console.ReadLine();

            Console.WriteLine("Въведете имейл на режисьора:");
            string email = Console.ReadLine();

            string zaqvka = "INSERT INTO Directors([name], [surname], [yearwork], [phone],[email]) VALUES (@Name,@Surname,@Yearwork,@Phone,@Email)";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(zaqvka, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@Name", firstname);
                        cmd.Parameters.AddWithValue("@Surname", surnamename);
                        cmd.Parameters.AddWithValue("@YearWork", yearswork);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Email", email);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Успешно добавихте запис в Directors");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void AddFilms()
        {
            Console.WriteLine("Въведете името на студиото на филма от тези, които са в Studios:");
            int studio_id = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете името на режисьора на филма от тези, които са в Directors:");
            int director_id = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете заглавие на филма:");
            string title = Console.ReadLine();

            Console.WriteLine("Въведете сценарист на филма:");
            string author = Console.ReadLine();

            Console.WriteLine("Въведете година на излизане на филма:");
            int year_published = int.Parse(Console.ReadLine());

            string zaqvka = "INSERT INTO Films(studio_id,director_id,title,author,year_published) VALUES (@Studio_id,@Director_id,@Title,@Author,@Year_published)";
            using (SqlConnection connection=new SqlConnection(StringConnection))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(zaqvka, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Studio_id", studio_id);
                        command.Parameters.AddWithValue("@Director_id", director_id);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Author", author);
                        command.Parameters.AddWithValue("@Year_published", year_published);

                        command.ExecuteNonQuery();
                        Console.WriteLine("Успешно добавихте филм във Films");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void AddActors()
        {
            Console.WriteLine("Въведете име на актьора:");
            string firstname = Console.ReadLine();

            Console.WriteLine("Въведете фамилия на актьора:");
            string surnamename = Console.ReadLine();

            Console.WriteLine("Въведете трудов стаж (в години) на актьора:");
            int yearswork = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете телефон за контакт на актьора:");
            string phone = Console.ReadLine();

            Console.WriteLine("Въведете имейл на актьора:");
            string email = Console.ReadLine();

            string zaqvka = "INSERT INTO Actors([name], [surname], [yearwork], [phone],[email]) VALUES (@Name,@Surname,@Yearwork,@Phone,@Email)";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(zaqvka, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@Name", firstname);
                        cmd.Parameters.AddWithValue("@Surname", surnamename);
                        cmd.Parameters.AddWithValue("@YearWork", yearswork);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Email", email);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Успешно добавихте запис в Actors");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void AddFilmsActors()
        {
            Console.WriteLine("Въведете името на актьора от тези, които са в Actors:");
            int actor_id = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете името на филма от тези, които са в Films:");
            int film_id = int.Parse(Console.ReadLine());

            string zaqvka = "INSERT INTO FilmsActors(actor_id,film_id) VALUES (@Actor_id,@Film_id)";
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Actor_id", actor_id);
                        command.Parameters.AddWithValue("@Film_id", film_id);
                        
                        command.ExecuteNonQuery();
                        Console.WriteLine("Успешно добавихте филм във FilmsActors");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void Zaqvka1()
        {
            Console.WriteLine("Actors:");
            string zaqvka = "SELECT * FROM Actors";
            using(SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]} {reader[5]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka2()
        {
            Console.WriteLine("Имената на режисьора на филма: (въведи филма)");
            string filmtitle = Console.ReadLine();
            Console.WriteLine("Резултат от търсенето:");
            string zaqvka = $"SELECT Directors.[name], Directors.[surname], Films.title FROM Directors JOIN Films ON Directors.director_id =Films.director_id WHERE(Films.title= @filmtitle)";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@filmtitle", filmtitle);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka3()
        {
            Console.WriteLine("Заглавието на филмите, на които режисьорите имат повече от 11 години трудов стаж");
            Console.WriteLine("Резултат от търсенето:");
            string zaqvka = "SELECT Films.title FROM Directors JOIN Films ON Directors.director_id =Films.director_id WHERE(Directors.yearwork>11)";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka4()
        {
            Console.WriteLine("Заглавието на филма, на най-опитния режисьор");
            Console.WriteLine("Резултат от търсенето:");
            string zaqvka = "SELECT Top 1 Films.title FROM Directors JOIN Films ON Directors.director_id =Films.director_id ORDER BY Directors.yearwork DESC";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka5()
        {
            Console.WriteLine("Заглавието на филма, име и фамилия на актьора, който участва във филма, име и фамилия на режисьора на филма, АКО актьора има повече от 3 години трудов стаж");
            Console.WriteLine("Резултат от търсенето:");
            string zaqvka = "SELECT Films.title, Actors.[name], Actors.surname , Directors.[name], Directors.surname FROM Films JOIN FilmsActors ON Films.film_id = FilmsActors.film_id JOIN Actors ON FilmsActors.actor_id = Actors.actor_id JOIN  Directors ON Films.director_id = Directors.director_id WHERE(Actors.yearwork>3)";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka6()
        {
            Console.WriteLine("Име и фамилия на актьора, който има повече от (въведи години) години трудов стаж");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Резултат от търсенето:");
            string zaqvka = $"SELECT [name],surname FROM Actors WHERE(Actors.yearwork>@age)";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@age", age);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka7()
        {
            Console.WriteLine("Име и фамилия на режисьора, който има (въведи години) години трудов стаж");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Резултат от търсенето:");
            string zaqvka = $"SELECT [name],surname FROM Directors WHERE(Directors.yearwork=@age)";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@age", age);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka8()
        {
            Console.WriteLine("Studios:");
            string zaqvka = "SELECT * FROM Studios";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka9()
        {
            Console.WriteLine("Directors:");
            string zaqvka = "SELECT * FROM Directors";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]} {reader[5]}");
                        }
                    }
                }
            }
        }

        static void Zaqvka10()
        {
            Console.WriteLine("Films:");
            string zaqvka = "SELECT * FROM Films";
            using (SqlConnection con = new SqlConnection(StringConnection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(zaqvka, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]} {reader[3]} {reader[4]} {reader[5]}");
                        }
                    }
                }
            }
        }
    }
}
