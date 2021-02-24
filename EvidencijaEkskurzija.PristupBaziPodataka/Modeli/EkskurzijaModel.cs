using System;
using System.Xml.Serialization;

namespace EvidencijaEkskurzija.PristupBaziPodataka.Modeli
{
	public class EkskurzijaModel
	{
		[XmlElement("Id")]
		public int Id { get; set; }

		[XmlElement("IdDestinacije")]
		public int IdDestinacije { get; set; }

		[XmlElement("Cena")]
		public int Cena { get; set; }

		[XmlElement("Datum")]
		public DateTime Datum { get; set; }

		[XmlElement("DaniBoravka")]
		public int DaniBoravka { get; set; }

		[XmlElement("NazivDestinacije")]
		public string NazivDestinacije { get; set; }
	}
}
