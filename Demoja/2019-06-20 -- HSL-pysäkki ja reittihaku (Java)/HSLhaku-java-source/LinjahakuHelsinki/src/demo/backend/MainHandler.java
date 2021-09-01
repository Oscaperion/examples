package demo.backend;

import java.util.ArrayList;
import java.util.List;

/**
 * Käsittelee Parser-luokan säilyttämää dataa ja tarjoaa ne MainWindowin
 * käyttöön
 * 
 * @author Oskari Oksanen, 20.6.2019
 */
public class MainHandler {

	// Kun MainWindowissa on suoritettu pysäkkien tai reittien haku, viimeisimmät
	// hakutulokset tallennetaan näihin taulukoihin. Tarkempia tietoja esittäessä
	// MainWindown hakee ne näistä taulukoista
	private StopInformation[] currentStops = null;
	private RouteInformation[] currentRoutes = null;

	private Parser par;

	/**
	 * Alustaa MainHandlerin
	 */
	public MainHandler() {
		// Alustetaan Parser
		par = new Parser();
	}

	/**
	 * Antaa viimeisimpänä hakutuloksena saadut reitit ja niiden tiedot
	 * 
	 * @return Reitit ja niiden tiedot
	 */
	public RouteInformation[] getCurrentRoutes() {
		return currentRoutes;
	}

	/**
	 * Antaa viimeisimpänä hakutuloksena saadut pysäkit ja niiden tiedot
	 * 
	 * @return Pysäkit ja niiden tiedot
	 */
	public StopInformation[] getCurrentStops() {
		return currentStops;
	}

	/**
	 * Antaa taulukon kaikkien pysäkkien nimistä
	 * 
	 * @return Kaikkien pysäkkien nimet
	 */
	public String[] getAllStations() {
		StopInformation[] pt = par.getAllStations();
		currentStops = pt;
		List<String> results = new ArrayList<String>();

		for (int k = 0; k < pt.length; k++) {
			results.add(pt[k].getFinnishName());
		}

		return results.toArray(new String[0]);
	}

	/**
	 * Hakee etsittävän hakusanan perusteella tiedot kaikista sopivista reiteistä ja
	 * niiden pysäkeistä.
	 * 
	 * @param searching Hakusana
	 * @return Hakusanan mukaiset reitit ja niiden tiedot
	 */
	public RouteInformation[] searchForStationsByRouteCode(String searching) {
		return par.searchForStopsByRouteCode(searching);
	}

	/*
	 * private void makeLinjaList() { List<String> linjat = new ArrayList<String>();
	 * 
	 * for (int i = 0; i < pysakit.length; i++) { String[] tmp =
	 * pysakit[i].getLinjat(); for (int j = 0; j < tmp.length; j++) { String tmp2 =
	 * tmp[j]; boolean isListed = false; for (int k = 0; k < linjat.size(); k++) {
	 * if (linjat.get(k).toLowerCase().contains(tmp2.toLowerCase())) { isListed =
	 * true; break; } } if (!isListed) { linjat.add(tmp2); } } }
	 * 
	 * this.linjat = linjat.toArray(new String[0]); }
	 */

	/*
	 * public String[] searchForRoutesByID(String searching) {
	 * 
	 * RouteInformation[] lt = searchForStationsByRouteCode(searching); List<String>
	 * results = new ArrayList<String>(); currentRoutes = lt;
	 * 
	 * for (int k = 0; k < lt.length; k++) { StopInformation[] pt =
	 * lt[k].getPysakit();
	 * 
	 * for (int m = 0; m < pt.length; m++) { results.add(pt[m].getFinnishName()); }
	 * }
	 * 
	 * return results.toArray(new String[0]);
	 * 
	 * 
	 * List<String> results = new ArrayList<String>();
	 * 
	 * String tmp = searching.trim().toLowerCase();
	 * 
	 * for (int i = 0; i < pysakit.length; i++) { PysakkiTiedot tmp2 = pysakit[i];
	 * String[] pysakit = tmp2.getLinjat(); for (int j = 0; j < pysakit.length; j++)
	 * { String tmp3 = pysakit[j]; if (tmp3.toLowerCase().contains(tmp)) {
	 * results.add(giveStationName(tmp2)); break; } } }
	 * 
	 * String[] returning = results.toArray(new String[0]);
	 * 
	 * return returning;
	 * 
	 * }
	 */

	/**
	 * Etsii ja palauttaa kaikki haetun ID:n mukaiset reittien nimet ja suunta.
	 * Samalla otetaan muistiin tarkemmat tiedot reiteistä MainWindowia varten
	 * 
	 * @param searching Hakusana. Ainoastaan ID:n mukaan, suuntaa ei oteta huomioon
	 * @return Hakusanan mukaisten reittien nimet
	 */
	public String[] searchForLinjasByID(String searching) {
		RouteInformation[] routes = searchForStationsByRouteCode(searching);
		currentRoutes = routes;
		String[] returning = new String[routes.length];

		for (int i = 0; i < routes.length; i++) {
			returning[i] = routes[i].getRouteID() + " (Suunta " + routes[i].getDirection() + ")";
		}

		return returning;
	}

	/*
	 * public LinjaTiedot[] searchForPysakkiByLinjaID2(String searching) {
	 * List<LinjaTiedot> results = new ArrayList<LinjaTiedot>();
	 * 
	 * String tmp = searching.trim().toLowerCase();
	 * 
	 * for (int i = 0; i < pysakit.length; i++) { PysakkiTiedot tmp2 = pysakit[i];
	 * String[] pysakit = tmp2.getLinjat(); for (int j = 0; j < pysakit.length; j++)
	 * { String tmp3 = pysakit[j]; if (tmp3.toLowerCase().contains(tmp)) {
	 * results.add(giveStationName(tmp2)); break; } } }
	 * 
	 * String[] returning = results.toArray(new String[0]);
	 * 
	 * return returning; }
	 */

	/**
	 * Etsii ja palauttaa kaikki hakusanan mukaiset pysäkkien nimet. Samalla otetaan
	 * muistiin tarkemmat tiedot reiteistä MainWindowia varten
	 * 
	 * @param searching Hakusana. Voidaan hakea sekä suomalaista ja ruotsalaista
	 *                  nimeä että tunnusta
	 * @return Hakusanan mukaisten pysäkkien nimet
	 */
	public String[] searchForPysakkiByName(String searching) {
		StopInformation[] pt = par.searchForStopsByName(searching);
		currentStops = pt;
		List<String> returning = new ArrayList<String>();

		for (int i = 0; i < pt.length; i++) {
			returning.add(pt[i].getFinnishName());
		}

		return returning.toArray(new String[0]);

		/*
		 * List<String> results = new ArrayList<String>();
		 * 
		 * String tmp = searching.trim().toLowerCase();
		 * 
		 * for (int i = 0; i < pysakit.length; i++) { PysakkiTiedot tmp2 = pysakit[i];
		 * String tmp3 = tmp2.getFinnishName() + " " + tmp2.getSwedishName(); if
		 * (tmp3.toLowerCase().contains(tmp)) { results.add(giveStationName(tmp2)); } }
		 * 
		 * String[] returning = results.toArray(new String[0]);
		 * 
		 * return returning;
		 */
	}

	/*
	 * public String[] searchForPysakkiByLinja(String searching) {
	 * 
	 * }
	 * 
	 * 
	 * private String giveStationName(PysakkiTiedot pt) { return pt.getFinnishName()
	 * + " (" + pt.getSwedishName() + ")"; }
	 */
}
