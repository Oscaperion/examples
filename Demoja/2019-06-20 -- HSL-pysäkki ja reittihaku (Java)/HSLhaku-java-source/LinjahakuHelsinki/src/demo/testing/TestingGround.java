package demo.testing;

public class TestingGround {

	public static void main(String[] args) {
		/*
		MainHandler mh = new MainHandler();
		
		mh.searchForPysakkiByName("Maria");
		//mh.searchForPysakkiByLinjaID("6911");
		*/
		
		demo.backend.Parser par = new demo.backend.Parser();
		 //par.searchForStationsByRouteCode("1087N");
		par.getAllStations();
	}

}
