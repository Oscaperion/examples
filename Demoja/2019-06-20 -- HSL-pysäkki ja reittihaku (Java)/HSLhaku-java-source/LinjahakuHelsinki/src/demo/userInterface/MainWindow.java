package demo.userInterface;

import java.awt.*;
import java.awt.event.*;

import demo.backend.MainHandler;
import demo.backend.RouteInformation;
import demo.backend.StopInformation;

/**
 * Ohjelman k‰yttˆliittym‰
 * 
 * @author Oskari Oksanen, 20.6.2019
 */
public class MainWindow extends Frame {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private MainHandler mainHandler;
	private boolean checkingStations = false;

	public static void main(String args[]) {
		// mh = new MainHandler();

		// Parser par = new Parser();
		// PysakkiTiedot[] pysakit = mh.getPysakkiTiedot();
		// LinjaTiedot[] lt = mh.searchForStationsByRouteCode("1001");

		new MainWindow();
	}

	public MainWindow() {
		// Alustetaan taustak‰sittelij‰
		mainHandler = new MainHandler();

		// Alustetaan ikkuna
		Frame mainFrame = new Frame();
		mainFrame.setTitle("HSL-pys‰kki- ja reittihaku");
		mainFrame.setResizable(false);

		// Alustetaan ikkunan sulkija
		mainFrame.addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosing(WindowEvent e) {
				mainFrame.dispose();
			}
		});

		Button searchButton = new Button("Etsi");
		searchButton.setBounds(250, 47, 80, 25);
		mainFrame.add(searchButton);

		TextField searchTextField = new TextField();
		searchTextField.setBounds(20, 50, 225, 20);
		mainFrame.add(searchTextField);

		// T‰m‰ n‰ytet‰‰n joka haun aluksi, vaikkakin viesti on relevantti vain
		// ensimm‰isen aikana
		Panel infoPanel = new Panel();
		infoPanel.setBounds(20, 100, 310, 230);
		// infoPanel.setBackground(Color.gray);
		infoPanel.add(new Label("HUOM! Ensimm‰inen haku voi kest‰‰"));
		infoPanel.add(new Label("parikymment‰kin sekuntia ennen tulosten esitt‰mist‰"));
		infoPanel.setVisible(false);
		mainFrame.add(infoPanel);

		String etsiPysakkia = "Etsi pys‰kki‰";
		String etsiReittia = "Etsi reitti‰";
		CheckboxGroup checkboxes = new CheckboxGroup();
		Checkbox checkBoxForStop = new Checkbox(etsiPysakkia, checkboxes, true);
		checkBoxForStop.setBounds(20, 70, 90, 30);
		Checkbox checkBoxForRoute = new Checkbox(etsiReittia, checkboxes, false);
		checkBoxForRoute.setBounds(120, 70, 200, 30);
		mainFrame.add(checkBoxForStop);
		mainFrame.add(checkBoxForRoute);

		// Mit‰ tapahtuu, kun Etsi-nappia painetaan
		searchButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				// Estet‰‰n alkuper‰isen n‰kym‰n komponenttien k‰sittely etsinn‰n ajaksi
				searchButton.setEnabled(false);
				searchTextField.setEnabled(false);
				checkBoxForStop.setEnabled(false);
				checkBoxForRoute.setEnabled(false);
				infoPanel.setVisible(true);

				String searching = searchTextField.getText().trim();
				String[] results;

				// Tarkistetaan, etsit‰‰nkˆ pys‰kki‰ vai reitti‰
				if (checkBoxForStop.getState()) {
					checkingStations = true;

					// Mik‰li ei ole kirjoitettu mit‰‰n, ohjelma listaa kaikki pys‰kit
					if (searching.contentEquals("")) {
						results = mainHandler.getAllStations();

						// Muuten etsit‰‰n hakusanan perusteella
					} else {
						results = mainHandler.searchForPysakkiByName(searching);
					}
				} else {
					checkingStations = false;
					results = mainHandler.searchForLinjasByID(searching);
				}

				TextArea textAreaForResults = new TextArea();
				// TextArea ta = new TextArea("Hello world!\r\nI am Gabe Newell");
				textAreaForResults.setBounds(20, 80, 310, 250);

				Choice resultSelector = new Choice();
				resultSelector.setBounds(20, 50, 220, 20);

				Button backButton = new Button("< Takaisin");
				backButton.setBounds(250, 47, 80, 25);

				// Listataan lˆydetyt hakutulokset valikkoon
				for (int i = 0; i < results.length; i++) {
					resultSelector.add(results[i]);
				}

				// Mit‰ tehd‰‰n, kun tulosta vaihdetaan valikosta
				resultSelector.addItemListener(new ItemListener() {
					public void itemStateChanged(ItemEvent arg0) {
						int chosenResultIndex = resultSelector.getSelectedIndex();
						String changedPrintOut = "";
						// Mik‰li n‰ytet‰‰n pys‰kkej‰, n‰ytet‰‰n valitun pys‰kin tiedot
						if (checkingStations) {
							StopInformation chosenStop = mainHandler.getCurrentStops()[chosenResultIndex];
							changedPrintOut = "Nimi: " + chosenStop.getFinnishName() + "\r\nNamn: "
									+ chosenStop.getSwedishName() + "\r\nTunnus: " + chosenStop.getSolmuTunnus();

							// Mik‰li n‰ytet‰‰n reittej‰, n‰ytet‰‰n reitin tiedot sek‰ sen sis‰lt‰m‰t
							// pys‰kit
						} else {
							RouteInformation chosenRoute = mainHandler.getCurrentRoutes()[chosenResultIndex];
							changedPrintOut = "Reitti " + chosenRoute.getRouteID() + " (Suunta "
									+ chosenRoute.getDirection() + ")";
							StopInformation[] routesStops = chosenRoute.getPysakit();
							for (int p = 0; p < routesStops.length; p++) {
								changedPrintOut = changedPrintOut + "\r\n\r\n" + (p + 1) + ".\r\nNimi: "
										+ routesStops[p].getFinnishName() + "\r\nNamn: "
										+ routesStops[p].getSwedishName() + "\r\nTunnus: "
										+ routesStops[p].getSolmuTunnus();
							}
						}
						textAreaForResults.setText(changedPrintOut);
					}
				});

				// Kun n‰kym‰‰ alustetaan, asetetaan sen hetkisen pys‰kin/reitin tiedot esiin
				String printOut = "";
				if (checkingStations) {
					StopInformation firstStop = mainHandler.getCurrentStops()[resultSelector.getSelectedIndex()];

					printOut = "Nimi: " + firstStop.getFinnishName() + "\r\nNamn: " + firstStop.getSwedishName()
							+ "\r\nTunnus: " + firstStop.getSolmuTunnus();
				} else {
					RouteInformation firstRoute = mainHandler.getCurrentRoutes()[resultSelector.getSelectedIndex()];

					printOut = "Reitti " + firstRoute.getRouteID() + " (Suunta " + firstRoute.getDirection() + ")";
					StopInformation[] firstRoutesStops = firstRoute.getPysakit();
					for (int p = 0; p < firstRoutesStops.length; p++) {
						printOut = printOut + "\r\n\r\n" + (p + 1) + ".\r\nNimi: "
								+ firstRoutesStops[p].getFinnishName() + "\r\nNamn: "
								+ firstRoutesStops[p].getSwedishName() + "\r\nTunnus: "
								+ firstRoutesStops[p].getSolmuTunnus();
					}
				}
				textAreaForResults.setText(printOut);

				// Etsint‰ on suoritettu, joten alkuper‰isen n‰kym‰n komponentit laitetaan
				// piiloon. Uudessa n‰kym‰ss‰ Takaisin-nappia painaessa n‰m‰ aluksi alustetut
				// komponentit asetetaan takaisin n‰kyviksi.
				searchTextField.setVisible(false);
				checkBoxForStop.setVisible(false);
				checkBoxForRoute.setVisible(false);
				searchButton.setVisible(false);
				infoPanel.setVisible(false);

				// Mit‰ tapahtuu Takaisin-nappia painettaessa
				backButton.addActionListener(new ActionListener() {
					public void actionPerformed(ActionEvent e) {
						searchTextField.setVisible(true);
						searchTextField.setText("");
						checkBoxForStop.setVisible(true);
						checkBoxForRoute.setVisible(true);
						searchButton.setVisible(true);

						searchButton.setEnabled(true);
						searchTextField.setEnabled(true);
						checkBoxForStop.setEnabled(true);
						checkBoxForRoute.setEnabled(true);

						mainFrame.remove(textAreaForResults);
						mainFrame.remove(backButton);
						mainFrame.remove(resultSelector);
					};
				});

				mainFrame.add(resultSelector);
				mainFrame.add(textAreaForResults);
				mainFrame.add(backButton);
			}
		});

		mainFrame.setSize(350, 360);
		mainFrame.setLayout(null);
		mainFrame.setVisible(true);
	}

	/*
	 * public MainWindow(StopInformation[] pt) { addWindowListener(new
	 * WindowAdapter() { public void windowClosing(WindowEvent e) { dispose(); } });
	 * 
	 * Frame f = new Frame();
	 * 
	 * Button b = new Button("click me"); b.setBounds(30, 50, 80, 30); f.add(b);
	 * 
	 * b.addActionListener(new ActionListener() { public void
	 * actionPerformed(ActionEvent e) { f.removeAll();
	 * 
	 * TextArea ta = new TextArea(addLinjat(pt)); ta.setBounds(10, 30, 300, 300);
	 * f.add(ta); } });
	 * 
	 * f.setSize(400, 400); f.setLayout(null); f.setVisible(true); }
	 * 
	 * public MainWindow(RouteInformation[] linjaTiedot) { Frame f = new Frame();
	 * Choice c = new Choice(); c.setBounds(20, 40, 300, 300);
	 * 
	 * StopInformation[] pt = linjaTiedot[0].getPysakit();
	 * 
	 * for (int i = 0; i < pt.length; i++) { c.add(pt[i].getFinnishName()); }
	 * 
	 * 
	 * // c.add("Optio 1"); c.add("Optio 2");
	 * 
	 * Panel panel = new Panel(); panel.setBounds(40, 80, 200, 200);
	 * panel.setBackground(Color.gray);
	 * 
	 * f.add(c); f.add(panel);
	 * 
	 * f.setSize(400, 400);
	 * 
	 * panel.add(new Label("lollero kuudestoista")); Label la = new
	 * Label("Hello, my name is Gaben"); la.setAlignment(Label.LEFT); panel.add(la);
	 * f.setLayout(null); f.setVisible(true);
	 * 
	 * c.addItemListener(new ItemListener() {
	 * 
	 * public void itemStateChanged(ItemEvent arg0) { String str = "" +
	 * c.getItem(c.getSelectedIndex()); la.setText(str); }
	 * 
	 * });
	 * 
	 * }
	 */

	/*
	 * public String addLinjat(StopInformation[] pysakit) { String returning = "";
	 * 
	 * for (int i = 0; i < pysakit.length; i++) { returning = returning +
	 * pysakit[i].getFinnishName() + "\r\n"; }
	 * 
	 * return returning; }
	 */
}
