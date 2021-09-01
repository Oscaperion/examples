package demo.backend;

import java.util.ArrayList;
import java.util.List;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import com.google.gson.JsonArray;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.google.gson.JsonPrimitive;

/**
 * Lukee ja s�ilytt�� ohjelman vaatimat JSON-tiedot
 * 
 * @author Oskari Oksanen, 20.6.2019
 */
public class Parser {

	private JsonObject stops, routes;

	public Parser() {
		stops = null;
		routes = null;
	}

	/**
	 * Mik�li haulla ei l�ytynyt sopivia pys�kkej� tai JSON-tiedostot eiv�t ole
	 * saatavilla, t�t� k�ytet��n sen sijaan
	 * 
	 * @return Tyhj� pys�kki
	 */
	private StopInformation[] giveEmptyStop() {
		StopInformation[] pt = { new StopInformation() };
		return pt;
	}

	/**
	 * Mik�li haulla ei l�ytynyt sopivia reittej� tai JSON-tiedostot eiv�t ole
	 * saatavilla, t�t� k�ytet��n sen sijaan
	 * 
	 * @return Tyhj� reitti
	 */
	private RouteInformation[] giveEmptyRoute() {
		StopInformation pt = new StopInformation();
		RouteInformation[] lt = { new RouteInformation() };
		lt[0].addStop(pt);
		return lt;
	}

	/**
	 * Palauttaa kaikkien pys�kkien tiedot
	 * 
	 * @return Pys�kkien tiedot
	 */
	public StopInformation[] getAllStations() {
		// Mik�li JSON-tiedostoja ei ole luettu eik� niit� ole s�il�tty, suorita
		// asianmukaiset toiminnot ennen pys�kkien tietojen esitt�mist�
		try {
			if (stops == null) {
				fetchJson();
			}
			// Mik�li tietoja hakiessa tulee ongelmia, palautetaan "tyhj�" pys�kki
		} catch (IOException e) {
			// TODO Auto-generated catch block
			// e.printStackTrace();
			return giveEmptyStop();
		}

		// Otetaan pys�kkien lista muistiin
		JsonArray tmp2 = stops.get("features").getAsJsonArray();
		int arrayLength = tmp2.size();
		// Alustetaan tyhj� taulukko pys�kkien tietoja varten
		StopInformation[] pt = new StopInformation[arrayLength];

		for (int i = 0; i < arrayLength; i++) {
			// Otetaan yksitt�isen pys�kin tiedot sis�lt�v� lista yl�s
			JsonObject tmp3 = tmp2.get(i).getAsJsonObject().get("properties").getAsJsonObject();

			// Otetaan aseman tiedot yl�s
			JsonPrimitive nimi1 = tmp3.get("NIMI1").getAsJsonPrimitive();
			JsonPrimitive namn1 = tmp3.get("NAMN1").getAsJsonPrimitive();
			JsonPrimitive nimi2 = tmp3.get("NIMI2").getAsJsonPrimitive();
			JsonPrimitive namn2 = tmp3.get("NAMN2").getAsJsonPrimitive();
			JsonPrimitive solmuTunnus = tmp3.get("SOLMUTUNNU").getAsJsonPrimitive();
			JsonPrimitive lyhytTunnus = tmp3.get("LYHYTTUNNU").getAsJsonPrimitive();

			// Luodaan uusi pys�kkitiedot sis�lt�v� muuttuja
			pt[i] = new StopInformation(nimi1, namn1, nimi2, namn2, solmuTunnus, lyhytTunnus);

		}

		return pt;
	}

	/**
	 * Etsii ja palautta hakusanan mukaiset pys�kit
	 * 
	 * @param searching Hakusana
	 * @return Hakusanan mukaiset pys�kit
	 */
	public StopInformation[] searchForStopsByName(String searching) {
		try {
			if (routes == null) {
				fetchJson();
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			// e.printStackTrace();
			return giveEmptyStop();
		}

		String searchTmp = searching.toLowerCase();

		List<StopInformation> returning = new ArrayList<StopInformation>();

		JsonArray tmp = stops.get("features").getAsJsonArray();
		int arrayLength = tmp.size();

		for (int i = 0; i < arrayLength; i++) {
			// Otetaan jokaisen yksitt�isen aseman tiedot muistiin k�sittely� varten
			JsonObject tmp2 = tmp.get(i).getAsJsonObject().get("properties").getAsJsonObject();

			// Otetaan aseman nimet yl�s. Muuttujat on asetettu seuraavasti:
			// nimi1: Paikan nimi. Voi joissain tilanteissa olla sama kuin nimi2
			// namn2: Paikan nimi ruotsiksi
			// nimi2: Paikan osoite.
			// namn2: Paikan osoite ruotsiksi.
			JsonPrimitive nimi1 = tmp2.get("NIMI1").getAsJsonPrimitive();
			JsonPrimitive namn1 = tmp2.get("NAMN1").getAsJsonPrimitive();
			JsonPrimitive nimi2 = tmp2.get("NIMI2").getAsJsonPrimitive();
			JsonPrimitive namn2 = tmp2.get("NAMN2").getAsJsonPrimitive();
			JsonPrimitive solmuTunnus = tmp2.get("SOLMUTUNNU").getAsJsonPrimitive();
			JsonPrimitive lyhytTunnus = tmp2.get("LYHYTTUNNU").getAsJsonPrimitive();

			String forSearch = nimi1.getAsString() + " " + namn1.getAsString() + " " + nimi2.getAsString() + " "
					+ namn2.getAsString() + " " + solmuTunnus.getAsString() + " " + lyhytTunnus.getAsString();

			if (forSearch.toLowerCase().contains(searchTmp)) {

				StopInformation newStation = new StopInformation(nimi1, namn1, nimi2, namn2, solmuTunnus, lyhytTunnus);
				returning.add(newStation);
			}
		}

		if (returning.size() == 0) {
			return giveEmptyStop();
		}

		return returning.toArray(new StopInformation[0]);
	}

	/**
	 * Etsii ja palautta hakusanan mukaiset pys�kit
	 * 
	 * @param searching Hakusana
	 * @return Hakusanan mukaiset reitit ja niiden pys�kit
	 */
	public RouteInformation[] searchForStopsByRouteCode(String searching) {

		try {
			if (routes == null) {
				fetchJson();
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		JsonArray tmp = routes.get("features").getAsJsonArray();
		int arrayLength = tmp.size();
		List<RouteInformation> linjat = new ArrayList<RouteInformation>();
		RouteInformation current = null;
		String currentRoute = "";
		int currentDirection = 0;
		boolean routeFound = false;

		for (int i = 0; i < arrayLength; i++) {
			JsonObject tmp2 = tmp.get(i).getAsJsonObject().get("properties").getAsJsonObject();

			String reittiTunnus = tmp2.get("REITTI").getAsJsonPrimitive().getAsString();
			int reittiSuunta = tmp2.get("SUUNTA").getAsJsonPrimitive().getAsInt();

			if (reittiTunnus.toLowerCase().contains(searching.toLowerCase())) {
				routeFound = true;

				if (current == null) {
					current = new RouteInformation(reittiTunnus, reittiSuunta);
					currentRoute = reittiTunnus;
					currentDirection = reittiSuunta;
				}

				if (!currentRoute.contentEquals(reittiTunnus)
						|| (currentRoute.contentEquals(reittiTunnus) && currentDirection != reittiSuunta)) {
					linjat.add(current);
					current = new RouteInformation(reittiTunnus, reittiSuunta);
					currentRoute = reittiTunnus;
					currentDirection = reittiSuunta;
				}

				if (currentRoute.contentEquals(reittiTunnus) && currentDirection == reittiSuunta) {
					JsonPrimitive nimi1 = tmp2.get("NIMI").getAsJsonPrimitive();
					JsonPrimitive namn1 = tmp2.get("NIMIR").getAsJsonPrimitive();
					JsonPrimitive nimi2 = tmp2.get("OSOITE").getAsJsonPrimitive();
					JsonPrimitive namn2 = tmp2.get("OSOITER").getAsJsonPrimitive();
					JsonPrimitive solmuTunnus = tmp2.get("SOLMUTUNNU").getAsJsonPrimitive();
					JsonPrimitive lyhytTunnus = tmp2.get("LYHYTTUNNU").getAsJsonPrimitive();

					StopInformation newPysakki = new StopInformation(nimi1, namn1, nimi2, namn2, solmuTunnus,
							lyhytTunnus);

					current.addStop(newPysakki);
				}
			} else if (routeFound) {
				linjat.add(current);
				current = null;
				routeFound = false;
			}
		}

		if (linjat.size() == 0) {
			return giveEmptyRoute();
		}

		RouteInformation[] linjaPalautus = linjat.toArray(new RouteInformation[0]);

		return linjaPalautus;
	}

	/**
	 * Hakee tarvittavat JSON-tiedostot - pelk�t pys�kit sek� reitit ja niiden
	 * pys�kit - ohjelman muistiin
	 * 
	 * @throws IOException Ilmenee mik�li vaadittavat tiedostot eiv�t ole saatavilla
	 *                     netist�
	 */
	private void fetchJson() throws IOException {
		// Pelk�t pys�kit
		URL HSL_pysakit = new URL("https://opendata.arcgis.com/datasets/b2aa879ce93c4068ac63b64d71f24947_0.geojson");

		// Reitit ja niihin kuuluvat pys�kit
		URL HSL_pysakitLinjoittain = new URL(
				"https://opendata.arcgis.com/datasets/c3a01a65b7a0467cba2a14935be8c2a2_0.geojson");

		// Haetaan JSON-tiedostot verkosta, luetaan ne ja tallennetaan ne muistiin
		InputStreamReader in = new InputStreamReader(HSL_pysakit.openStream());
		stops = new JsonParser().parse(in).getAsJsonObject();
		in = new InputStreamReader(HSL_pysakitLinjoittain.openStream());
		routes = new JsonParser().parse(in).getAsJsonObject();

		// Suljetaan InputStreamReader
		in.close();
	}

	/*
	 * // Testausta varten tehty aliohjelma, ei ole varsinaisen ohjelmiston k�yt�ss�
	 * private static void testParsing() throws IOException { // Lue Helsingin
	 * pys�kkitietoja sis�lt�v� API, linkattu osoitteessa: //
	 * https://public-transport-hslhrt.opendata.arcgis.com/datasets/hsln-pys%C3%
	 * A4kit/data // (Viitattu 13.6.2019)
	 * 
	 * // try {
	 * 
	 * int numero = 3; numero++;
	 * 
	 * // JsonObject tmp = new JsonParser().parse(new //
	 * FileReader("C:\\Users\\Oskari\\Dropbox\\ty�\\solteq\\HSL.geojson")).
	 * getAsJsonObject();
	 * 
	 * // T�M� TOIMII
	 * 
	 * //
	 * https://stackoverflow.com/questions/6259339/how-to-read-a-text-file-directly-
	 * from-internet-using-java
	 * 
	 * // Pys�kit URL HSL_pysakit = new URL(
	 * "https://opendata.arcgis.com/datasets/b2aa879ce93c4068ac63b64d71f24947_0.geojson"
	 * );
	 * 
	 * // Pys�kit linjoittain URL HSL_pysakitLinjoittain = new URL(
	 * "https://opendata.arcgis.com/datasets/c3a01a65b7a0467cba2a14935be8c2a2_0.geojson"
	 * );
	 * 
	 * InputStreamReader in = new InputStreamReader(HSL_pysakit.openStream()); //
	 * https://static.javadoc.io/com.google.code.gson/gson/2.8.5/com/google/gson/
	 * JsonParser.html JsonObject pelkatPysakit = new
	 * JsonParser().parse(in).getAsJsonObject();
	 * 
	 * in = new InputStreamReader(HSL_pysakitLinjoittain.openStream());
	 * 
	 * JsonObject pysakitLinjoineen = new JsonParser().parse(in).getAsJsonObject();
	 * 
	 * in.close();
	 * 
	 * JsonArray tmp2 = pelkatPysakit.get("features").getAsJsonArray(); int
	 * arrayLength = tmp2.size(); PysakkiTiedot[] pt = new
	 * PysakkiTiedot[arrayLength];
	 * 
	 * for (int i = 0; i < arrayLength; i++) { JsonObject tmp3 =
	 * tmp2.get(i).getAsJsonObject().get("properties").getAsJsonObject();
	 * 
	 * // Pelk�t pys�kit JsonPrimitive nimi1 =
	 * tmp3.get("NIMI1").getAsJsonPrimitive(); JsonPrimitive namn1 =
	 * tmp3.get("NAMN1").getAsJsonPrimitive(); JsonPrimitive nimi2 =
	 * tmp3.get("NIMI2").getAsJsonPrimitive(); JsonPrimitive namn2 =
	 * tmp3.get("NAMN2").getAsJsonPrimitive(); JsonPrimitive solmuTunnus =
	 * tmp3.get("SOLMUTUNNU").getAsJsonPrimitive(); JsonPrimitive lyhytTunnus =
	 * tmp3.get("LYHYTTUNNU").getAsJsonPrimitive();
	 * 
	 * // Pys�kit linjoittain
	 * 
	 * JsonPrimitive nimi1 = tmp3.get("NIMI").getAsJsonPrimitive(); JsonPrimitive
	 * namn1 = tmp3.get("NIMIR").getAsJsonPrimitive(); JsonPrimitive nimi2 =
	 * tmp3.get("OSOITE").getAsJsonPrimitive(); JsonPrimitive namn2 =
	 * tmp3.get("OSOITER").getAsJsonPrimitive(); JsonPrimitive solmuTunnus =
	 * tmp3.get("SOLMUTUNNU").getAsJsonPrimitive(); JsonPrimitive lyhytTunnus =
	 * tmp3.get("LYHYTTUNNU").getAsJsonPrimitive();
	 * 
	 * 
	 * pt[i] = new PysakkiTiedot(nimi1, namn1, nimi2, namn2, solmuTunnus,
	 * lyhytTunnus); numero++; }
	 * 
	 * tmp2 = pysakitLinjoineen.get("features").getAsJsonArray(); arrayLength =
	 * tmp2.size();
	 * 
	 * for (int j = 0; j < arrayLength; j++) { JsonObject tmp3 =
	 * tmp2.get(j).getAsJsonObject().get("properties").getAsJsonObject();
	 * 
	 * String reittiTunnus = tmp3.get("REITTI").getAsJsonPrimitive().getAsString();
	 * String solmuTunnus =
	 * tmp3.get("SOLMUTUNNU").getAsJsonPrimitive().getAsString(); String lyhytTunnus
	 * = tmp3.get("LYHYTTUNNU").getAsJsonPrimitive().getAsString();
	 * 
	 * for (int k = 0; k < pt.length; k++) { if
	 * (solmuTunnus.contains(pt[k].getSolmuTunnus()) &&
	 * lyhytTunnus.contains(pt[k].getLyhytTunnus())) { //
	 * pt[k].addLinja(reittiTunnus); break; } } }
	 * 
	 * numero++;
	 * 
	 * // } catch (IOException e) { // TODO Auto-generated catch block //
	 * e.printStackTrace(); // }
	 * 
	 * }
	 */

}