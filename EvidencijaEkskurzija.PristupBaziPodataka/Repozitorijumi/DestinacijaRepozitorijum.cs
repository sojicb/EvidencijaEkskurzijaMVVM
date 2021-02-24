using EvidencijaEkskurzija.PristupBaziPodataka.Modeli;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EvidencijaEkskurzija.PristupBaziPodataka.Repozitorijumi
{
	public class DestinacijaRepozitorijum
	{
		private readonly string _konekcioniString;

		public DestinacijaRepozitorijum(string konekcioniString)
		{
			_konekcioniString = konekcioniString;
		}


		public DestinacijaModel Get(int id)
		{
			DestinacijaModel destinacija = new DestinacijaModel();

			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "SELECT * FROM Destinacija WHERE Id = @id";
					sqlCommand.Parameters.AddWithValue("@id", id);

					using (SqlDataReader reader = sqlCommand.ExecuteReader())
					{
						while (reader.Read())
						{
							destinacija.Id = (int)reader["Id"];
							destinacija.Naziv = reader["Naziv"] as string;
						}
					}
				}
			}
			return destinacija;
		}

		public List<DestinacijaModel> GetAllDestinacije()
		{
			List<DestinacijaModel> destinacije = new List<DestinacijaModel>();

			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "SELECT * FROM Destinacija";

					using (SqlDataReader reader = sqlCommand.ExecuteReader())
					{
						while (reader.Read())
						{
							destinacije.Add(new DestinacijaModel
							{
								Id = (int)reader["Id"],
								Naziv = reader["Naziv"] as string
							});
						}
					}
				}
			}
			return destinacije;
		}

		public DestinacijaModel Delete(int id)
		{
			using(SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using(SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "DELETE FROM Destinacija WHERE Id = @id";
					sqlCommand.Parameters.AddWithValue("@id", id);
					sqlCommand.ExecuteNonQuery();

					return sqlCommand.ExecuteScalar() as DestinacijaModel;
				}
			}
		}

		public int Add(DestinacijaModel model)
		{
			using(SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "INSERT INTO Destinacija(Naziv) OUTPUT Inserted.Id " +
						"VALUES(@Naziv)"; ;
					sqlCommand.Parameters.AddWithValue("@Naziv", model.Naziv);

					return (int)sqlCommand.ExecuteScalar();
				}
			}
		}
	}
}
