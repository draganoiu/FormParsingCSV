# FormParsingCSV

# Task Result

## Used technologies / Frameworks

I use the following technologies / frameworks in my project:

- C#
- .NET Framework 4.7.2
- I use Microsoft SQL Server Management Studio for database(SQL)

I choose .net framework and windows forms application because simplfy common application tasks such as reading and writing to the file system.
I used Visual Studio because you can create Windows Forms smart-client applications that display information, request input from users.

## Used 3rd Party Libraries


I do not used 3rd Party Libraries becuase visual studio and projects like windows forms cotains all I need to do this tak.

## Installation / Run

Describe how we can check your project locally.
In order to use this project you must:
1.Install the visual studio with .NET framework component from :https://visualstudio.microsoft.com/downloads/
2.Clone or download the project :https://github.com/draganoiu/FormParsingCSV.git
3.Install a tool for sql to create an empty database with the following columns 
Hauptartikelnr,Artikelname,Hersteller,Beschreibung,Materialangaben,Geschlecht,Produktart,Ärmel,Bein,Kragen,Herstellung,Taschenart,Grammatur,Material,Ursprungsland,Bildname as nvarchar(max)
4.Open the Visual Studio.
5.Open the project.
6.Open the Form1.cs 
7.Whenever you see the "using (SqlConnection conn = new SqlConnection(" Server = 192.168.12.202; Database = TestDB; User Id = sa; Password = Clarity123"))" modify the  "Server" and "Database" with yours
8.Press F5 to run the project.
9.Browse the CSV file and press Import.
10.After Import the csv data are processed and inserted into database and first 17 records are displayed.
11.You can also insert some records into database using the button ADD.
