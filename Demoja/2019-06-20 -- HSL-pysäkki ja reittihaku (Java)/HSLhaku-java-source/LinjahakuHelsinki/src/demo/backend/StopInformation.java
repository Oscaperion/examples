package demo.backend;

import com.google.gson.JsonPrimitive;

/**
 * S‰ilytt‰‰ tietoa yksitt‰isest‰ pys‰kist‰
 * 
 * @author Oskari Oksanen, 20.6.2019
 */
public class StopInformation {
	private String suomiNimi, ruotsiNimi, solmuTunnus, lyhytTunnus;
	// private List<String> linjat = new ArrayList<String>();

	/**
	 * Alustetaan uusi pys‰kki JsonPrimitive-muuttujien pohjalta
	 * 
	 * @param nimi1       Pys‰kin nimi suomeksi
	 * @param namn1       Pys‰kin nimi ruotsiksi
	 * @param nimi2       Pys‰kin osoite suomeksi
	 * @param namn2       Pys‰kin osoite ruotsiksi
	 * @param solmuTunnus Pys‰kin solmutunnus
	 * @param lyhytTunnus Pys‰kin lyhyttunnus
	 */
	public StopInformation(JsonPrimitive nimi1, JsonPrimitive namn1, JsonPrimitive nimi2, JsonPrimitive namn2,
			JsonPrimitive solmuTunnus, JsonPrimitive lyhytTunnus) {
		setNames(nimi1, namn1, nimi2, namn2);
		setID(solmuTunnus, lyhytTunnus);
	}

	/**
	 * Alustetaan uusi pys‰kki String-muuttujien pohjalta
	 * 
	 * @param nimi1       Pys‰kin nimi suomeksi
	 * @param namn1       Pys‰kin nimi ruotsiksi
	 * @param nimi2       Pys‰kin osoite suomeksi
	 * @param namn2       Pys‰kin osoite ruotsiksi
	 * @param solmuTunnus Pys‰kin solmutunnus
	 * @param lyhytTunnus Pys‰kin lyhyttunnus
	 */
	public StopInformation(String nimi1, String namn1, String nimi2, String namn2, String solmuTunnus,
			String lyhytTunnus) {
		setNames(nimi1, namn1, nimi2, namn2);
		setID(solmuTunnus, lyhytTunnus);
	}

	/**
	 * Alustetaan tyhj‰ pys‰kki. K‰ytet‰‰n virhetilanteissa
	 */
	public StopInformation() {
		setNullValues();
	}

	/**
	 * Asetetaan tyhj‰t arvot
	 */
	private void setNullValues() {
		String nullMessage = "(NULL)";

		suomiNimi = nullMessage;
		ruotsiNimi = nullMessage;
		solmuTunnus = nullMessage;
		lyhytTunnus = nullMessage;
		// linjat.add(nullMessage);
	}

	/**
	 * Asetetaan pys‰kin nimi
	 * 
	 * @param nimi1 Pys‰kin nimi suomeksi
	 * @param namn1 Pys‰kin nimi ruotsiksi
	 * @param nimi2 Pys‰kin osoite suomeksi
	 * @param namn2 Pys‰kin osoite ruotsiksi
	 */
	private void setNames(JsonPrimitive nimi1, JsonPrimitive namn1, JsonPrimitive nimi2, JsonPrimitive namn2) {
		setNames(nimi1.getAsString(), namn1.getAsString(), nimi2.getAsString(), namn2.getAsString());
	}

	/**
	 * Asetetaan pys‰kin nimi
	 * 
	 * @param nimi1 Pys‰kin nimi suomeksi
	 * @param namn1 Pys‰kin nimi ruotsiksi
	 * @param nimi2 Pys‰kin osoite suomeksi
	 * @param namn2 Pys‰kin osoite ruotsiksi
	 */
	private void setNames(String nimi1, String namn1, String nimi2, String namn2) {
		suomiNimi = nimi1 + ", " + nimi2;
		ruotsiNimi = namn1 + ", " + namn2;
	}

	/**
	 * Asetetaan pys‰kin tunnukset
	 * 
	 * @param solmuT Pys‰kin solmutunnus
	 * @param lyhytT Pys‰kin lyhyttunnus
	 */
	private void setID(JsonPrimitive solmuT, JsonPrimitive lyhytT) {
		setID(solmuT.getAsString(), lyhytT.getAsString());
	}

	/**
	 * Asetetaan pys‰kin tunnukset
	 * 
	 * @param solmuT Pys‰kin solmutunnus
	 * @param lyhytT Pys‰kin lyhyttunnus
	 */
	private void setID(String solmuT, String lyhytT) {
		solmuTunnus = solmuT;
		lyhytTunnus = lyhytT;
	}

	/**
	 * Palautetaan pys‰kin nimi suomeksi
	 * 
	 * @return Pys‰kin nimi
	 */
	public String getFinnishName() {
		return suomiNimi;
	}

	/**
	 * Palautetaan pys‰kin nimi ruotsiksi
	 * 
	 * @return Pys‰kin nimi
	 */
	public String getSwedishName() {
		return ruotsiNimi;
	}

	/**
	 * Palautetaan pys‰kin solmutunnus
	 * 
	 * @return Pys‰kin solmutunnus
	 */
	public String getSolmuTunnus() {
		return solmuTunnus;
	}

	/**
	 * Palautetaan pys‰kin lyhyttunnus
	 * 
	 * @return Pys‰kin lyhyttunnus
	 */
	public String getLyhytTunnus() {
		return lyhytTunnus;
	}

	/*
	 * public void addLinja(String linjaTunnus) { if (!isLinjaListed(linjaTunnus)) {
	 * linjat.add(linjaTunnus); } }
	 */

	/*
	 * private boolean isLinjaListed(String linjaTunnus) { for (int i = 0; i <
	 * linjat.size(); i++) { if (linjaTunnus.contains(linjat.get(i))) { return true;
	 * } } return false; }
	 */

	/*
	 * public String[] getLinjat() { return linjat.toArray(new String[0]); }
	 */
}
