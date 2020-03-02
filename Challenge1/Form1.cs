using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challenge1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}


		private void btnBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".csv";
			ofd.Filter = "Comma Separated (*.csv)|*.csv"; 
			ofd.ShowDialog();

			txtFileName.Text = ofd.FileName;

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{


			DataTable importData = GetDataFromFile();


			if (importData == null) return;

			SaveImportDataToDatabase(importData);
			MessageBox.Show("Import is successfully");

		}

		private DataTable GetDataFromFile()
		{
			DataTable importedData = new DataTable();
			try
			{   // Open the text file using a stream reader.
				using (StreamReader sr = new StreamReader(txtFileName.Text))
				{
					string header = sr.ReadLine();
					if (string.IsNullOrEmpty(header))
					{
						MessageBox.Show("No file data");
						return null;
					}
					//regex for parsing the string between the ; and ignore it in the ""
					Regex CSVParser = new Regex(";(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
					string[] headerColumns = CSVParser.Split(header);

					foreach (string headerColumn in headerColumns)
					{
						importedData.Columns.Add(headerColumn);
					}

					// Read the stream line by line until I reach the .jpg
					while (!sr.EndOfStream)
					{
						StringBuilder line = new StringBuilder(sr.ReadLine());
						if (line.Length == 0) continue;

						while (!line.ToString().Contains(".jpg"))
							line.Append(sr.ReadLine());
						string[] fields = CSVParser.Split(line.ToString());
						DataRow importedRow = importedData.NewRow();

						for (int i = 0; i < fields.Count(); i++)
						{
							importedRow[i] = fields[i];
						}

						importedData.Rows.Add(importedRow);
					}
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}

			return importedData;
		}
		//pushing the values imported to the database
		private void SaveImportDataToDatabase(DataTable importData)
		{

			using (SqlConnection conn = new SqlConnection(" Server = 192.168.12.202; Database = TestDB; User Id = sa; Password = Clarity123"))
			{
				conn.Open();
				int i = 0;
				foreach (DataRow importRow in importData.Rows)
				{
					//inserting only 17 records because it takes to much time
					if (i < 17)
					{
						SqlCommand cmd = new SqlCommand("INSERT INTO Product4" +
					"(Hauptartikelnr,Artikelname,Hersteller,Beschreibung,Materialangaben,Geschlecht,Produktart,Ärmel,Bein,Kragen,Herstellung,Taschenart,Grammatur,Material,Ursprungsland,Bildname)" +
					"VALUES (@haup,@artikelname,@hersteller,@beschreinbug,@materialpangaben,@geschlecht,@producktart,@armel,@bein,@kragen,@herstellung,@taschenart,@grammaturv,@material,@ursprungland,@bildname)", conn);

						cmd.Parameters.AddWithValue("@haup", importRow["Hauptartikelnr"]);
						cmd.Parameters.AddWithValue("@artikelname", importRow["Artikelname"]);
						cmd.Parameters.AddWithValue("@hersteller", importRow["Hersteller"]);
						cmd.Parameters.AddWithValue("@beschreinbug", importRow["Beschreibung"]);
						cmd.Parameters.AddWithValue("@materialpangaben", importRow["Materialangaben"]);
						cmd.Parameters.AddWithValue("@geschlecht", importRow["Geschlecht"]);

						cmd.Parameters.AddWithValue("@producktart", importRow["Produktart"]);
						cmd.Parameters.AddWithValue("@armel", importRow["Ärmel"]);
						cmd.Parameters.AddWithValue("@bein", importRow["Bein"]);
						cmd.Parameters.AddWithValue("@kragen", importRow["Kragen"]);
						cmd.Parameters.AddWithValue("@herstellung", importRow["Herstellung"]);
						cmd.Parameters.AddWithValue("@taschenart", importRow["Taschenart"]);
						cmd.Parameters.AddWithValue("@grammaturv", importRow["Grammatur"]);
						cmd.Parameters.AddWithValue("@material", importRow["Material"]);
						cmd.Parameters.AddWithValue("@ursprungland", importRow["Ursprungsland"]);
						cmd.Parameters.AddWithValue("@bildname", importRow["Bildname"]);
						cmd.ExecuteNonQuery();

						//inserting data in the view model
						dataGridView1.Rows.Add();
						dataGridView1.Rows[i].Cells[0].Value = importRow["Hauptartikelnr"].ToString();
						dataGridView1.Rows[i].Cells[1].Value = importRow["Artikelname"].ToString();
						dataGridView1.Rows[i].Cells[2].Value = importRow["Hersteller"].ToString();
						dataGridView1.Rows[i].Cells[3].Value = importRow["Beschreibung"].ToString();
						dataGridView1.Rows[i].Cells[4].Value = importRow["Materialangaben"].ToString();
						dataGridView1.Rows[i].Cells[5].Value = importRow["Geschlecht"].ToString();
						dataGridView1.Rows[i].Cells[6].Value = importRow["Produktart"].ToString();
						dataGridView1.Rows[i].Cells[7].Value = importRow["Ärmel"].ToString();
						dataGridView1.Rows[i].Cells[8].Value = importRow["Bein"].ToString();
						dataGridView1.Rows[i].Cells[9].Value = importRow["Kragen"].ToString();
						dataGridView1.Rows[i].Cells[10].Value = importRow["Herstellung"].ToString();
						dataGridView1.Rows[i].Cells[11].Value = importRow["Taschenart"].ToString();
						dataGridView1.Rows[i].Cells[12].Value = importRow["Grammatur"].ToString();
						dataGridView1.Rows[i].Cells[13].Value = importRow["Material"].ToString();
						dataGridView1.Rows[i].Cells[14].Value = importRow["Ursprungsland"].ToString();
						dataGridView1.Rows[i].Cells[15].Value = importRow["Bildname"].ToString();


						i++;
					}
				}


			}
		}

		private void textBox14_TextChanged(object sender, EventArgs e)
		{

		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			//pushing the new records into database
			using (SqlConnection conn = new SqlConnection(" Server = 192.168.12.202; Database = TestDB; User Id = sa; Password = Clarity123"))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("INSERT INTO Product4 values('" + Haup.Text + "','" + Artik.Text + "','" + Hers.Text + "','" + Besch.Text + "','" + Mate.Text + "','" + Gesc.Text + "','" + Prod.Text + "','" + Arme.Text + "','" + Be.Text + "','" + Kr.Text + "','" + Herst.Text + "','" + Tasch.Text + "','" + Gramm.Text + "','" + Materi.Text + "','" + Ursprun.Text + "','" + Bildn.Text + "' )", conn);
				cmd.ExecuteNonQuery();
				MessageBox.Show("Data has saved in Product4 Database");
				
			}
			

		}

		private void Hauptartikelnr_Click(object sender, EventArgs e)
		{

		}

		private void Ursprungsland_Click(object sender, EventArgs e)
		{

		}

		private void ReadCSVFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		
		}
	}
}
