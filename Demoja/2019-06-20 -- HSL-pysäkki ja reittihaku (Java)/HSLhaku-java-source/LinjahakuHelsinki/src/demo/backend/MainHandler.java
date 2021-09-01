package demo.backend;

import java.util.ArrayList;
import java.util.List;

/**
 * K�sittelee Parser-luokan s�ilytt�m�� dataa ja tarjoaa ne MainWindowin
 * k�ytt��n
 * 
 * @author Oskari Oksanen, 20.6.2019
 */
public class MainHandler {

	// Kun MainWindowissa on suoritettu pys�kkien tai reittien haku, viimeisimm�t
	// hakutulokset tallennetaan n�ihin taulukoihin. Tarkempia tietoja esitt�ess�
	// MainWindown hakee ne n�ist� taulukoista
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
	 * Antaa viimeisimp�n� hakutuloksena saadut reitit ja niiden tiedot
	 * 
	 * @return Reitit ja niiden tiedot
	 */
	public RouteInformation[] getCurrentRoutes() {
		return currentRoutes;
	}

	/**
	 * Antaa viimeisimp�n� hakutuloksena saadut pys�kit ja niiden tiedot
	 * 
	 * @return Pys�kit ja niiden tiedot
	 */
	public StopInformation[] getCurrentStops() {
		return currentStops;
	}

	/**
	 * Antaa taulukon kaikkien pys�kkien nimist�
	 * 
	 * @return Kaikkien pys�kkien nimet
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
	 * Hakee etsitt�v�n hakusanan perusteella tiedot kaikista sopivista reiteist� ja
	 * niiden pys�keist�.
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
	 * Samalla otetaan muistiin tarkemmat tiedot reiteist� MainWindowia varten
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
	 * Etsii ja palauttaa kaikki hakusanan mukaiset pys�kkien nimet. Samalla otetaan
	 * muistiin tarkemmat tiedot reiteist� MainWindowia varten
	 * 
	 * @param searching Hakusana. Voidaan hakea sek� suomalaista ja ruotsalaista
	 *                  nime� ett� tunnusta
	 * @return Hakusanan mukaisten pys�kkien nimet
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
