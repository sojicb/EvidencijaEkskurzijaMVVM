using EvidencijaEkskurzija.PristupBaziPodataka.Modeli;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EvidencijaEkskurzija.PristupBaziPodataka.Repozitorijumi
{
	public class EkskurzijaRepozitorijum
	{
		private readonly string _konekcioniString;

		public EkskurzijaRepozitorijum(string konekcioniString)
		{
			_konekcioniString = konekcioniString;
		}

		public EkskurzijaModel Get(int id)
		{
			EkskurzijaModel ekskurzija = new EkskurzijaModel();

			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "SELECT * FROM Ekskurzija WHERE Id = @id";
					sqlCommand.Parameters.AddWithValue("@id", id);

					using (SqlDataReader reader = sqlCommand.ExecuteReader())
					{
						while (reader.Read())
						{
							ekskurzija.Id = (int)reader["Id"];
							ekskurzija.IdDestinacije = (int)reader["IdDestinacije"];
							ekskurzija.Cena = (int)reader["Cena"];
							ekskurzija.Datum = (DateTime)reader["Naziv"];
							ekskurzija.DaniBoravka = (int)reader["DaniBoravka"];
						}
					}
				}
			}
			return ekskurzija;
		}

		public List<EkskurzijaModel> GetAllEkskurzije()
		{
			List<EkskurzijaModel> ekskurzije = new List<EkskurzijaModel>();

			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "SELECT Ekskurzija.Id, Ekskurzija.IdDestinacije, Ekskurzija.Cena, Ekskurzija.DaniBoravka, Ekskurzija.Datum, Destinacija.Naziv FROM Ekskurzija " +
											"INNER JOIN Destinacija on Ekskurzija.IdDestinacije = Destinacija.Id";

					using (SqlDataReader reader = sqlCommand.ExecuteReader())
					{
						while (reader.Read())
						{
							ekskurzije.Add(new EkskurzijaModel
							{
								Id = (int)reader["Id"],
								IdDestinacije = (int)reader["IdDestinacije"],
								NazivDestinacije = reader["Naziv"] as string,
								Cena = (int)reader["Cena"],
								Datum = (DateTime)reader["Datum"],
								DaniBoravka = (int)reader["DaniBoravka"]
							});
						}
					}
				}
			}

			return ekskurzije;
		}

		public List<EkskurzijaModel> PretragaEkskurzije(string pretraga)
		{
			List<EkskurzijaModel> ekskurzije = new List<EkskurzijaModel>();

			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "SELECT Ekskurzija.Id, Ekskurzija.IdDestinacije, Ekskurzija.Cena, Ekskurzija.DaniBoravka, Ekskurzija.Datum, Destinacija.Naziv FROM Ekskurzija " +
											"INNER JOIN Destinacija on Ekskurzija.IdDestinacije = Destinacija.Id WHERE Destinacija.Naziv LIKE '%' + @pretraga + '%'";
					sqlCommand.Parameters.AddWithValue("@pretraga", pretraga);

					using (SqlDataReader reader = sqlCommand.ExecuteReader())
					{
						while (reader.Read())
						{
							ekskurzije.Add(new EkskurzijaModel
							{
								Id = (int)reader["Id"],
								IdDestinacije = (int)reader["IdDestinacije"],
								NazivDestinacije = reader["Naziv"] as string,
								Cena = (int)reader["Cena"],
								Datum = (DateTime)reader["Datum"],
								DaniBoravka = (int)reader["DaniBoravka"]
							});
						}
					}
				}
			}

			return ekskurzije;
		}

		public EkskurzijaModel Delete(int id)
		{
			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "DELETE FROM Ekskurzija WHERE Id = @id";
					sqlCommand.Parameters.AddWithValue("@id", id);
					sqlCommand.ExecuteNonQuery();

					return sqlCommand.ExecuteScalar() as EkskurzijaModel;
				}
			}
		}

		public int Add(EkskurzijaModel model)
		{
			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "INSERT INTO Ekskurzija(IdDestinacije, Cena, Datum, DaniBoravka) OUTPUT Inserted.Id " +
						"VALUES(@IdDestinacije, @Cena, @Datum, @DaniBoravka)"; ;
					sqlCommand.Parameters.AddWithValue("@IdDestinacije", model.IdDestinacije);
					sqlCommand.Parameters.AddWithValue("@Cena", model.Cena);
					sqlCommand.Parameters.AddWithValue("@Datum", model.Datum);
					sqlCommand.Parameters.AddWithValue("@DaniBoravka", model.DaniBoravka);

					return (int)sqlCommand.ExecuteScalar();
				}
			}
		}

		public int Update(EkskurzijaModel model)
		{
			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "UPDATE Ekskurzija SET IdDestinacije=@IdDestinacije, Cena=@Cena, Datum=@Datum, DaniBoravka=@DaniBoravka OUTPUT Inserted.Id WHERE Id=@id"; ;
					sqlCommand.Parameters.AddWithValue("@id", model.Id);
					sqlCommand.Parameters.AddWithValue("@IdDestinacije", model.IdDestinacije);
					sqlCommand.Parameters.AddWithValue("@Cena", model.Cena);
					sqlCommand.Parameters.AddWithValue("@Datum", model.Datum);
					sqlCommand.Parameters.AddWithValue("@DaniBoravka", model.DaniBoravka);

					return (int)sqlCommand.ExecuteScalar();
				}
			}
		}
	}
}
