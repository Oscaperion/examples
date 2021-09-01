package demo.backend;

import java.util.ArrayList;
import java.util.List;

/**
 * S‰ilytt‰‰ tietoa yksitt‰isest‰ reitist‰ sek‰ sen sis‰lt‰mist‰ pys‰keist‰.
 * Samaa reitti‰ eri suuntiin k‰sitell‰‰n myˆs eri reittein‰
 * 
 * @author Oskari Oksanen, 20.6.2019
 */
public class RouteInformation {
	private String reittiID = "";
	private int suuntaID = -1;
	private List<StopInformation> pysakit = new ArrayList<StopInformation>();

	/**
	 * Alustetaan tyhj‰ reitti.
	 */
	public RouteInformation() {
		reittiID = "(NULL)";
	}

	/**
	 * Alustetaan uusi reitti String-muuttjien pohjalta. S‰ilytt‰‰ tarkemmat tiedot
	 * reitist‰ ja sen pys‰keist‰
	 * 
	 * @param reittiID Reitin ID
	 * @param suuntaID Suuntaa esitt‰v‰ kokonaisluku, voi olla joko 1 tai 2
	 */
	public RouteInformation(String reittiID, int suuntaID) {
		this.reittiID = reittiID;
		this.suuntaID = suuntaID;
	}

	/**
	 * Lis‰‰ uusi pys‰kki reittiin
	 * 
	 * @param pysakki Lis‰tt‰v‰ pys‰kki
	 */
	public void addStop(StopInformation pysakki) {
		pysakit.add(pysakki);
	}

	/**
	 * Palautetaan taulukko kaikista reitin sis‰lt‰mist‰ pys‰keist‰
	 * 
	 * @return Taulukko pys‰keist‰
	 */
	public StopInformation[] getPysakit() {
		return pysakit.toArray(new StopInformation[0]);
	}

	/**
	 * Palauttaa reitin tunnuksen
	 * 
	 * @return Reitin tunnus
	 */
	public String getRouteID() {
		return reittiID;
	}

	/**
	 * Palauttaa reitin suunnan
	 * 
	 * @return Reitin suunta. Normaalisti 1 tai 2, tyhj‰n reitin tapauksessa -1
	 */
	public int getDirection() {
		return suuntaID;
	}
}
