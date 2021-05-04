

var latLng_Zero = L.latLng(28.4588446, 77.2867589);
const map = L.map('fieldMap', { minZoom: 3, maxZoom: 20}).setView(latLng_Zero, 18);   //The initial point
var osmlayer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
var mqilayer = new L.TileLayer('http://{s}.mqcdn.com/tiles/1.0.0/sat/{z}/{x}/{y}.png', {subdomains: ['otile1','otile2','otile3','otile4']});
//var nasa = new L.TileLayer('http://tileserver.maptiler.com/nasa/{z}/{x}/{y}.jpg');
var nasa = new L.TileLayer('http://otile4.mqcdn.com/tiles/1.0.0/map/{z}/{X}/{Y}.png');
		
		
		
		
//var newLayer = new OpenLayers.Layer.OSM("New Layer", "URL_TO_TILES/${z}/${x}/${y}.png", {numZoomLevels: 19});

//var baseMaps = {
//    "OpenStreetMap": osmlayer,
//    "MapQuestImagery": mqilayer
//};

//var overlays =  {//add any overlays here
//    };

//map.addLayer(osmlayer);
//map.addControl(L.control.layers(baseMaps,overlays, {position: 'bottomleft'}));

//map.addLayer(osmlayer); // Adding layer to the map
enableHighAccuracy=true;



//var veloroad = L.tileLayer('http://tile.osmz.ru/veloroad/{z}/{x}/{y}.png', { attribution: 'Map &copy; OpenStreetMap | Tiles &copy Ilya Zverev' });
//var osmlayer = L.tileLayer('http://tile.openstreetmap.org/{z}/{x}/{y}.png', { attribution: 'Map &copy; OpenStreetMap' });
map.addLayer(osmlayer);
map.addControl(L.control.layers({'MQI': mqilayer, 'OSM': osmlayer, 'NASA': nasa}));

var marker_arr = [];
var marker_pos = [];
var marker_new = [];

var tempLine = null;


///////////////////
// ADD markers :
//var a = new L.LatLng(28.4588446, 77.2867589),
//    b = new L.LatLng(28.4578446, 77.2867589),
//    c = new L.LatLng(28.45788446, 77.2877589);

marker_pos[0] = new L.LatLng(28.4588446, 77.2867589);
marker_pos[1] = new L.LatLng(28.4578446, 77.2867589);
marker_pos[2] = new L.LatLng(28.45788446, 77.2877589);


var locationIcon = L.icon({
    iconUrl: 'img/location.png',
    iconSize: [32, 32],
    iconAnchor: [16, 16]
    //  popupAnchor: [-3, -76],
    //  shadowUrl: 'my-icon-shadow.png',
    //  shadowSize: [68, 95],
    //  shadowAnchor: [22, 94]
});
var tractorIcon = L.icon({
    iconUrl: 'img/tractor.png',
    iconSize: [32, 50],
    iconAnchor: [16, 25]
    //  popupAnchor: [-3, -76],
    //  shadowUrl: 'my-icon-shadow.png',
    //  shadowSize: [68, 95],
    //  shadowAnchor: [22, 94]
});

//var marker_a = new L.Marker(a, { icon: tractorIcon, draggable: true }).addTo(map);
//marker_b = new L.Marker(b, { icon: locationIcon, draggable: true }).addTo(map);
//marker_c = new L.Marker(c, { icon: locationIcon, draggable: true }).addTo(map);
// Put marker for TRACTOR on the map
marker_arr[0] = new L.Marker(marker_pos[0], { icon: tractorIcon, draggable: true }).addTo(map);
marker_arr[1] = new L.Marker(marker_pos[1], { icon: locationIcon, draggable: true }).addTo(map);
marker_arr[2] = new L.Marker(marker_pos[2], { icon: locationIcon, draggable: true }).addTo(map);
// Put polyline for TRACTOR on the map
var polyline_demo = new L.Polyline([marker_pos[0], marker_pos[1], marker_pos[2]]).addTo(map);  // make the polyline as well
marker_arr[0].parentLine = polyline_demo;   // Important. this is linking the markers to the particular polyline
marker_arr[1].parentLine = polyline_demo;	// ribbon effect !!!
marker_arr[2].parentLine = polyline_demo;

// chanhe the TRACTOR markers opacity in one shot:
var myGroup = L.layerGroup([marker_arr[0], marker_arr[1], marker_arr[2]]);
myGroup.eachLayer(function (osmlayer) { osmlayer.setOpacity(0.5)});

//L.marker([50.505, 30.57], { icon: myIcon }).addTo(map);
//marker = L.marker([28.4598446,77.2867589], { icon: tractorIcon, draggable: true }).addTo(map).on('click', onTractor);


// Now on dragstart you'll need to find the latlng from the polyline which corresponds
// with your marker's latlng and store it's key in your marker instance so you can use it later on:
function dragStartHandler(e) {
    var polyline = e.target.parentLine;
    if (polyline) {
        var latlngPoly = polyline.getLatLngs(),     // Get the polyline's latlngs
            //var latlngPoly = polyline3.getLatLngs() // *** NOT WORKING !
            latlngMarker = this.getLatLng();        // Get the actual, cliked MARKER's start latlng
        console.log("start");
        for (var i = 0; i < latlngPoly.length; i++) {       // Iterate the polyline's latlngs
            if (latlngMarker.equals(latlngPoly[i])) {       // Compare marker's latlng ot the each polylines 
                this.polylineLatlng = i;            // If equals store key in thos global variable

                wayPoints[i][0] = latlngPoly[i].lat;  // store the dragged, new coordinates in the matrix as well
                wayPoints[i][1] = latlngPoly[i].lng;
            }
        }
    }
}

// Now you know the key of the polyline's latlng you can change it
// when dragging the marker on the dragevent:
function dragHandler(e) {
    var polyline = e.target.parentLine;
    if (polyline) {
        var latlngPoly = e.target.parentLine.getLatLngs(),    // Get the polyline's latlngs
            //var latlngPoly = polyline3.getLatLngs() // *** NOT WORKING !    
            latlngMarker = this.getLatLng();            // Get the marker's current latlng
        console.log("drag");
        latlngPoly.splice(this.polylineLatlng, 1, latlngMarker);        // Replace the old latlng with the new
        polyline.setLatLngs(latlngPoly);           // Update the polyline with the new latlngs
        //polyline3.setLatLngs(latlngPoly);     // *** NOT WORKING !
		
    }
}

// Just to be clean and tidy remove the stored key on dragend:
function dragEndHandler(e) {
    delete this.polylineLatlng;
	update_table("dataTable");
    console.log("end");
	
}

//You'll need to attach eventlisteners and callbacks to your L.Marker's.
// You could automate this, but i'll keep it simple for now:
// https://stackoverflow.com/questions/33513404/leaflet-how-to-match-marker-and-polyline-on-drag-and-drop
// This is for TRACTOR movement:
marker_arr[0]
    .on('dragstart', dragStartHandler)
    .on('drag', dragHandler)
    .on('dragend', dragEndHandler);

marker_arr[1]
    .on('dragstart', dragStartHandler)
    .on('drag', dragHandler)
    .on('dragend', dragEndHandler);

marker_arr[2]
    .on('dragstart', dragStartHandler)
    .on('drag', dragHandler)
    .on('dragend', dragEndHandler);

///////////////////////////////////////////////////////////////////////////////
var doRouting = false;
var startPoint = [28.4588446, 77.2867589];
var arr = [];  //Arreay for routing lines
var pointCount = 0;

// make a new 2D matrix
var wayPoints = new Array(100);
// Loop to create 2D matrix using 1D matrix 
for (var i = 0; i < wayPoints.length; i++) {
    wayPoints[i] = new Array(2);
}


function routingTab() {
    //var property = document.getElementById("myRouting");

    removeActiveTab();
    document.getElementById("routing-tab").className = "nav-link active";

    // Change panel visibility
    document.getElementById("flight-data-panel").style.visibility = "hidden";
    document.getElementById("mission-plan-panel").style.display = "none";
    document.getElementById("geofence-panel").style.display = "none";
    document.getElementById("import-export-panel").style.display = "none";
    document.getElementById("camera-panel").style.display = "none";
    document.getElementById("routing-panel").style.display = "block";
    document.getElementById("connect-panel").style.display = "none";
	document.getElementById("tools-panel").style.display = "none";
	doRouting = true;
}

//*********************************************
//************* Click on MAP ******************
//*********************************************

map.on("click", function (e) {  //Listener: Click on MAP -> Addign the WAYPOINTS
if (doRouting == true)
    mapOn_Click_DoRouting(e);

if (doMeasuring == true)  //measure distance
	mapOn_Click_DoMesuring(e);

if (doAutoTracing == true)  //measure distance
	mapOn_Click_DoAutoTracing(e);

if (doArea == true)  //measure distance
	mapOn_Click_DoArea(e);
//if (areaSelected==true){  //drown rectangle
//}	


})  // end map on


//******************************************
//************ Do Routing ******************

function mapOn_Click_DoRouting(e) {
	if (!tempLine) {
            tempLine = L.polyline([], { color: 'blue', noClip: true }).addTo(map);
        }
        tempLine.addLatLng(e.latlng);
        startPoint = [e.latlng.lat, e.latlng.lng];  // save the new points for the starter of the next point
 
        addRow('dataTable', startPoint[0], startPoint[1]); // add one row on the panel
        // store bule line coordinates for later comunication (make green line, table etc.)
        wayPoints[pointCount][0] = startPoint[0];
        wayPoints[pointCount][1] = startPoint[1];

        //L.circleMarker([e.latlng.lat, e.latlng.lng]).addTo(map);  //move the Waypoint
        // ***** add marker to the map. Long string !   ********
        marker_new[pointCount] = new L.marker([e.latlng.lat, e.latlng.lng], { icon: locationIcon, draggable: true, opacity: 0.4 }).addTo(map).on('dragstart', dragStartHandler).on('drag', dragHandler).on('dragend', dragEndHandler);
        marker_new[pointCount].parentLine = tempLine;
		marker_new[pointCount]._id = pointCount  //### required, becuse later this will give the refrence to delete it.
        pointCount++;
 
        console.log(arr);    
	
}

function addRow(tableID, latit, longit) {
    var table = document.getElementById(tableID);
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    var cell2 = row.insertCell(0);  //waypoint
    cell2.innerHTML = rowCount;

    var cell3 = row.insertCell(1);
    cell3.innerHTML = latit.toFixed(5);  // take only the last 4 digits

    var cell4 = row.insertCell(2);
    cell4.innerHTML = longit.toFixed(5);

    //document.getElementById(element4).innerText="ahoj";
}

function delete_row(tableID) {
    try {
        var table = document.getElementById(tableID);
        var rowCount = table.rows.length;
        if (rowCount > 1) {
			rowCount=rowCount-1;
			delteLastWp(rowCount-1);
            table.deleteRow(rowCount);
			
			pointCount=pointCount-1;
			
        }
     } catch (e) {
        alert(e);
    }
}

function delteLastWp(id)  // ## with the last segment of the polyline 
{ //marker_new[pointCount]

	var new_markers = []
	marker_new.forEach(function(marker) {
		if (marker._id == id) {
			map.removeLayer(marker);
		}
		else new_markers.push(marker)
	})
	
	  marker_new = new_markers

	 var latlngs = tempLine.getLatLngs()
    latlngs.splice(-1); 
    tempLine.setLatLngs(latlngs);
		
}

function update_table(tableID) {   //update table after setting the mission in routing
    // first delete the rowas expect the header
    // add new rown acc. 
    // newPoly[pointCount][0] = startPoint[0];
    // newPoly[pointCount][1] = startPoint[1];

    try {
        var table = document.getElementById(tableID);
        var totalRows = table.rows.length; 
		var rowCount = table.rows.length;  // this will decrease
        
        for (var i = 1; i < rowCount; i++) {
            table.deleteRow(i);
            rowCount--;
            i--;
        }
    } catch (e) {
        alert(e);
    }

   for (var i = 1; i < totalRows; i++) {
        var row = table.insertRow(i);
        var cell2 = row.insertCell(0);  //first column for waypoint
        cell2.innerHTML = i;
        var latit = wayPoints[i - 1][0];		//container of the waipoint's polyline start &next is the end
        var longit = wayPoints[i - 1][1];

        var cell3 = row.insertCell(1);
        cell3.innerHTML = latit.toFixed(5);

        var cell4 = row.insertCell(2);
        cell4.innerHTML = longit.toFixed(5);
    }
}


function fillup_table(tableID, totalRows) {   //this function is not in use
    try {
        var table = document.getElementById(tableID);
    
    } catch (e) {
        alert(e);
    }

    for (var i = 1; i < totalRows+1; i++) {
        var row = table.insertRow(i);
        var cell2 = row.insertCell(0);  //first column for waypoint
        cell2.innerHTML = i;
        var latit = wayPoints[i - 1][0];		//container of the waipoint's polyline start &next is the end
        var longit = wayPoints[i - 1][1];
		
		//var x = parseFloat(latit);
		
        var cell3 = row.insertCell(1);
        cell3.innerHTML = latit.toFixed(5);

        var cell4 = row.insertCell(2);
        cell4.innerHTML = longit.toFixed(5);
    }
}


function set_mission() {
//Enable button	
//update_table("dataTable"); //just update the lat long table if there was some marker movment
	const button = document.getElementById('export-mission');
	button.disabled = false;
	console.log("Set mission");
	
	 console.log('Line count: ');
        console.log(pointCount);

        //make one polyline from clickings, which shall be draggable later:
        // ****** THIS IS NOT MOVING IF I CHANGE it in the HANDLERS *******//

        var polyline = tempLine.setStyle({ color: '#00AA00', weight: 10, opacity: 0.4 });
        tempLine = null;
		doRouting = false;
}

//************ End Routing ******************
//******************************************



//*******************************************
//************ Serial port ******************

function connectTab() {   //connect to serial port
    var property = document.getElementById("myConnetion");
    // setup();
    removeActiveTab();
    document.getElementById("connect-tab").className = "nav-link active";

    // Change panel visibility
    document.getElementById("flight-data-panel").style.visibility = "hidden";
    document.getElementById("mission-plan-panel").style.display = "none";
    document.getElementById("geofence-panel").style.display = "none";
    document.getElementById("import-export-panel").style.display = "none";
    document.getElementById("camera-panel").style.display = "none";
    document.getElementById("routing-panel").style.display = "none";
    document.getElementById("connect-panel").style.display = "block";
	document.getElementById("tools-panel").style.display = "none";
}


//**********************************************
//************ Export mission ******************

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) 
        month = '0' + month;
    if (day.length < 2) 
        day = '0' + day;

    return [year, month, day].join('-');
}


function export_mission_btn()  {
// save the mission	
//var userInput = document.getElementById("myText").value;
	var today = new Date();
	var date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();

	date = formatDate(date);
	var time = today.getHours() + "-" + today.getMinutes() + "-" + today.getSeconds();
	var dateTime = date+'_'+time;
	
	var outString_temp="";
	var outString="";
	
	for (i=0; i<pointCount; i++){
		var x,y;
		x=wayPoints[i][0].toString();
		y=wayPoints[i][1].toString();
		outString_temp= x +"\t"+ y+"\r\n";
		outString=	outString+outString_temp;
	}
				
	//use filesaver.js
	var blob = new Blob([outString], { type: "text/plain;charset=utf-8" });
	saveAs(blob, dateTime+".txt");
	
}
	
//************ End Export mission ******************
//**********************************************


//**********************************************
//************ Import mission (modal) **********

var marker_imp;
function import_waypoints() {  // this is putting the makers & polyline for modal's "output" see in html
	
	var inputText;
	inputText=document.getElementById('output').textContent;
	var res = inputText.split("\r\n"); // last row is "" -> issue in polyline !
	res.pop();  // remove last element = ""
	//var l = res.lenght;
	var wp=res;   
 
	for (i=0; i<res.length; i++){   // put markers on the map
		wp[i]=res[i].split("\t");
		var x=wp[i][0];
		var y=wp[i][1];
		marker_new[i] = new L.marker([x, y], {draggable: true, opacity: 0.4 }).addTo(map).on('dragstart', dragStartHandler).on('drag', dragHandler).on('dragend', dragEndHandler);
		
		wayPoints[i][0]=parseFloat(x);		//required for dataTable update
        wayPoints[i][1]=parseFloat(y);		// convert string to float
	
	}
	
// ### issue here:
	var myPolyline = L.polyline(wp, {color: 'red', draggable: true}).addTo(map);

	for (i=0; i<res.length; i++){ 
		marker_new[i].parentLine = myPolyline;  //this is doing the ribbon effect !!!
	}

	console.log(wp[0][0]);
	console.log(wp[0][1]);
	
	fillup_table("dataTable", res.length); //add data in the table on Routing panel
	pointCount=res.length;  // for adding more markers by clicking and anble to export later the imported waypoints as well

}


// this is a listenre in importMissionModal row # 348
document.getElementById('import-mission').addEventListener('change', function() 
{ 
// https://www.geeksforgeeks.org/how-to-read-a-local-text-file-using-javascript/
	var fr=new FileReader(); 
	var file = event.target.files[0];
	
	fr.onload=function(){ 
		document.getElementById('output').textContent=fr.result; // step #4 select and load the file content
						// step #5 put 'output' in in html secton 'importMissionModal' 
						
		import_waypoints();  //split the content of 'output' to coordinates of the waypoints			
	}
	fr.readAsText(file);
}) 



//**********************************************
//************ TOOLS ***************************
//**********************************************

function toolsTab() {   //open button Tools panel
  //  var property = document.getElementById("myConnetion");
    // setup();
    removeActiveTab();
    document.getElementById("tools-tab").className = "nav-link active";

    // Change panel visibility
    document.getElementById("flight-data-panel").style.visibility = "hidden";
    document.getElementById("mission-plan-panel").style.display = "none";
    document.getElementById("geofence-panel").style.display = "none";
    document.getElementById("import-export-panel").style.display = "none";
    document.getElementById("camera-panel").style.display = "none";
    document.getElementById("routing-panel").style.display = "none";
    document.getElementById("connect-panel").style.display = "none";
	document.getElementById("tools-panel").style.display = "block";
}

//**********************************************
//************ Distance Measuring **************
var
  _firstLatLng,
  _firstPoint,
  _secondLatLng,
  _secondPoint,
  _distance,
  _length,
  _polyline;
  
var doMeasuring=false;

function btnMeasure() {  //coming from button in html
	doMeasuring = true;
}

function mapOn_Click_DoMesuring(e) {  // coming from row #167
	if (!_firstLatLng) {
    _firstLatLng = e.latlng;
    L.marker(_firstLatLng).addTo(map).bindPopup('Point A<br/>' + e.latlng).openPopup();
  } else {
    _secondLatLng = e.latlng;
    L.marker(_secondLatLng).addTo(map).bindPopup('Point B<br/>' + e.latlng).openPopup();
  }

  if (_firstLatLng && _secondLatLng) {
    // draw the line between points
    L.polyline([_firstLatLng, _secondLatLng], {
      color: 'red'
    }).addTo(map);

    refreshDistanceAndLength();
	doMeasuring = false;
	_firstLatLng=true;
	
  }
}

// map.on('zoomend', function(e) {
//  refreshDistanceAndLength();
// })


function refreshDistanceAndLength() {
   _length = getDistance(_firstLatLng, _secondLatLng);
 document.getElementById('length').innerHTML = _length.toFixed(2);
 // alert("Distance: "+_length.toFixed(2));
}


//https://stackoverflow.com/questions/43167417/calculate-distance-between-two-points-in-leaflet

function getDistance(kezd, veg) {
var origin =[];
var destination = [];

	origin[0]=kezd.lat;  // conver LatLong type to array
	origin[1]=kezd.lng;
	destination[0]=veg.lat;
	destination[1]=veg.lng;
	// return distance in meters
    var lat1 = toRadian(origin[0]),
		lon1 = toRadian(origin[1]),
        lat2 = toRadian(destination[0]),
        lon2 = toRadian(destination[1]);
        

    var deltaLat = lat2 - lat1;
    var deltaLon = lon2 - lon1;

    var a = Math.pow(Math.sin(deltaLat/2), 2) + Math.cos(lat1) * Math.cos(lat2) * Math.pow(Math.sin(deltaLon/2), 2);
    var c = 2 * Math.asin(Math.sqrt(a));
    var EARTH_RADIUS = 6371;
    return c * EARTH_RADIUS * 1000;
}

function toRadian(degree) {
    return degree*Math.PI/180;
}


//**********************************************
//************ Auto Tracing ********************
var doAutoTracing = false;
function btnAutoTrace(){  //coming from button in html
	doAutoTracing=true;
}
	
function mapOn_Click_DoAutoTracing() {
var currentPoint = map.latLngToContainerPoint(latLng_Zero);  // convet geographical coordinate to pixel coordinate
var width = 100;
var height = 50;
var xDifference = width / 2;
var yDifference = height / 2;
// put rectangel as center
//var southWest = L.point((currentPoint.x - xDifference), (currentPoint.y - yDifference));  //Represents a point with x and y coordinates in pixels.
//var northEast = L.point((currentPoint.x + xDifference), (currentPoint.y + yDifference));

var southWest = L.point((currentPoint.x), (currentPoint.y));  //Represents a point with x and y coordinates in pixels.
var northEast = L.point((currentPoint.x + width), (currentPoint.y + height));


var bounds = L.latLngBounds(map.containerPointToLatLng(southWest),map.containerPointToLatLng(northEast));
								//map. .. : convert pixel coorinate to geogrphical
	
    L.rectangle(bounds).addTo(map);  //** Add rectangle to the map
	
var pointA = map.layerPointToLatLng(southWest);
var pointB = map.layerPointToLatLng(northEast);
var pointList = [pointA, pointB];
var tarcePolyline = [];

// **** make the TRACE ****
var ox = 0; //offset in X direction.
var oy = 0; //offset in Y direction.
var h=height-2*oy;   //Height
var w=width-2*ox;	// width
var d=10;	//distance between lines, cutterbar length !
var step=(w/d)/2;


for (i=0; i<step; i++){
	
	var p1 = L.point((currentPoint.x + 2*d*i + ox), (currentPoint.y + oy));
	var p1g = map.layerPointToLatLng(p1);  //convert pixel coorinate to geographical
	
	var p2 = L.point((currentPoint.x + 2*d*i + ox), (currentPoint.y +h + oy));
	var p2g = map.layerPointToLatLng(p2);	//convert pixel coorinate to geographical
	
	var p3 = L.point((currentPoint.x + d*(i*2+1) + ox), (currentPoint.y + h + oy));
	var p3g = map.layerPointToLatLng(p3);	//convert pixel coorinate to geographical
	
	var p4 = L.point((currentPoint.x + d*(i*2+1) + ox), (currentPoint.y) + oy);
	var p4g = map.layerPointToLatLng(p4);	//convert pixel coorinate to geographical
	
	var p5 = L.point((currentPoint.x + d*(i*2+2) + ox), (currentPoint.y) + oy);
	var p5g = map.layerPointToLatLng(p5);	//convert pixel coorinate to geographical
	
	pointList = [p1g, p2g, p3g, p4g, p5g];  // geographical coordinates
	
	tarcePolyline[i] = new L.Polyline(pointList, {  
		color: 'red',
		weight: 3,
		opacity: 0.5,
		smoothFactor: 1
	});
	tarcePolyline[i].addTo(map);	// *** draw the full section
}	

doAutoTracing=false;
}

//**********************************************
//*************** Area ************************ ribbon rectange, no tracing
var doArea = false;


function btnArea(){  //coming from button in html
	doArea=true;
	
}

function mapOn_Click_DoArea(e){
	var rect = L.rectangle([latLng_Zero, [28.4578446, 77.2865589]], { color: 'green'});
	map.addLayer(rect);
	rect.editing.enable();
	doArea=false;  // not allowing with second click on the map a new area

} 

function btnOff(){   //not active
	getRectCoord;
	rect.editing.disable();
	map.removeLayer(rect);
}

function getRectCoord() {
	var bbox = rect.getBounds(),					//Returns the LatLngBounds of the path.
		left = L.Util.formatNum(bbox.getWest(), 4),  //formatNum: Returns the number num rounded to digits decimals, or to 6 decimals by default.
		right = L.Util.formatNum(bbox.getEast(), 4),
		ttop = L.Util.formatNum(bbox.getNorth(), 4),
		bottom = L.Util.formatNum(bbox.getSouth(), 4);
		console.log(left, bottom, right, ttop);
		
		coordStr = left + '_' + bottom + '_' + right + '_' + ttop;
		document.getElementById('coordinates').innerHTML = 'left_bottom_right_top: ' + coordStr	;
		
//var topLeft = map.project(rect.getBounds().getNorthWest(), 18),
//		bottomRight = map.project(rect.getBounds().getSouthEast(), 18),
	
}

//rect.on('edit', getRectCoord);
//map.on('zoomend', getRectCoord);

//**********************************************
//*************** FIELD TRACE  ***************** ribbon geofence & ribbon auto trace
//var doArea = false;

function btnFieldTrace(){  //coming from button in html
//	doArea=true;
var nrMarkers = pointCount;


var poly = new Array(nrMarkers);   // geofence waypoints
//var StartPoint = new Array(1);
var tp = new Array(1000);   // !!//Turning Points inside the gofence
//var tp = [];
for (var i = 0; i < tp.length; i++)  //// Loop to create 2D matrix using 1D matrix 
    tp[i] = new Array(2);
var tpCounter=0;	// turning point counter

// var latLng_Zero = L.latLng(28.4588446, 77.2867589);

var ww = marker_new[0];   // just for rial / debug
//wayPoints[i][0]=parseFloat(x);		//required for dataTable update
//wayPoints[i][1]=parseFloat(y);		// convert string to float

// *******************************************
// ** The border points are in wayPoints[i] **
for (i=0; i<pointCount; i++) {
	 poly[i]=map.latLngToContainerPoint(wayPoints[i]); // !!! convet geofence waypoint's geographical coordinate to pixel coordinate
 }
	 
var pp = map.latLngToContainerPoint(wayPoints[pointCount-1]); // let make the last point as starting point
poly.pop(); // Remove the last element of an array
n = poly.length; 

pointCount=pointCount-1; // the last point was the starting point. Not part of geofence !

// ******* Point defs *********
//p = new Point(250, 200);    // **** the Initial point !! if the point is on the line, then inside !
//pStart=new Point(250, 200);
p =pp;
pStart=pp;
// **** Fige out the max widthd & height of the polygon for proper steps (stepsX & stepsY) calulation:
var minX=10000, minY=10000;
var maxX=0, maxY=0;
//for (i=0; i<pointCount; i++)
	for (j=0; j<pointCount; j++){
		var Ap=poly[j];   //Actual point in the geofence (pixel coordinates)
		if (Ap.x > maxX)  // Max points
			maxX = Ap.x;
		if (Ap.y > maxY)
			maxY = Ap.y;
		if (Ap.x < minX)  // Min points
			minX = Ap.x;
		if (Ap.y < minY)
			minY = Ap.y;
		}
var width = (maxX-minX);
var height = (maxY-minY);

var distX=10;	// distance of the steps in X & Y direction, cutter bar width
var distY=10;
var stepsX=parseInt(width/(2*distX)-1); 	// to be claualted, based on width (bound) of the polyline 2*:Up & Dpwn
var stepsY=parseInt(height/(2*distY)-1);	// to be claualted, based on heiht (bound) of the polyline  2*: Left & right
											// -1: may be the 4 sequence is not in the geofance

//------
 
var nrOfTurns=0;  // To calulate the sum
var totalDist=0;  // Sum of total distances ot calulate the distance of he trace
var movingDir ='H';  // Chage this for H/V movement

// *********************************
// ****** Horizontal diretion ******
if (movingDir=='H'){
/*	var distX=10;	// can be more than cutterbar width, this is segment in vertical distane
	var distY=10;	// cutter bar width
	var stepsX=13; 	// to be calculated, based on width (bound) of the polyline 
	var stepsY=23;	// to be calculated, based on heiht (bound) of the polyline 

*/	
//	stepsY=3; //=10
	pStart.x = p.x;
	pStart.y = p.y;
	tp[tpCounter][0]=pStart.x; tp[tpCounter][1]=pStart.y;
	tpCounter++;

for (j=0; j<stepsY; j++){		 // Y direction`

// (1) ### move   ------>
		while (isInside(...poly)){  // Is the 'p' point inside the polygon ?
			p.x=p.x + distX;
			totalDist = totalDist + distX;
		}
		p.x=p.x - distX;
		totalDist = totalDist - distX;
		
		//ctx.moveTo(pStart.x, p.y);  // make a line form begining till the last inside point
		tp[tpCounter][0]=pStart.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		
		//ctx.lineTo(p.x, p.y); 	// Make the first long line  //### STORE IN 'pixelTraceCoord' array
		//ctx.stroke()
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		
// (2) ### 1st turing point:
		nrOfTurns ++;
		p.y=p.y + distY;	
		totalDist = totalDist + distY;  //If it is out of Geofence, may be ....
		
		p.x=p.x + 20*distX;   //sweep from right to left to find the inside point
		while (!isInside(...poly)){  							//... this is never ending
			p.x=p.x - distX;
		}
		//ctx.lineTo(p.x, p.y);			//### STORE IN 'pixelTraceCoord' array
		//ctx.stroke();
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		
		pStart.x = p.x;   // Store the starting poinn for retur line
		
// (3) ### move <------
		while (isInside(...poly)){  
			p.x=p.x - distX;
			totalDist = totalDist + distX;
		}
		p.x=p.x + distX;
		totalDist = totalDist - distX;

		//ctx.moveTo(pStart.x, p.y); 
		tp[tpCounter][0]=pStart.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		
		//ctx.lineTo(p.x, p.y);		// Make the second long line    //### STORE IN 'pixelTraceCoord' array
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		
// (4) ### 2nd turing point:
		nrOfTurns ++;
		p.y=p.y+distY;			
		totalDist = totalDist + distY;					//If it is out of Geofence, may be ....
		
		p.x=p.x - 5*distX;   //sweep from left ot right to find the inside point
		var outsideCounter =0;
		while (!isInside(...poly)){			//... this is never ending
			p.x=p.x + distX;
			outsideCounter++;
		}
		//ctx.lineTo(p.x, p.y);		//### STORE IN 'pixelTraceCoord' array
		//ctx.stroke();
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		
		pStart.x = p.x;

	} // End for
} // End Horizontal


// *********************************
// ****** Vertical diretion ******
if (movingDir=='V'){
//var dist=10;	// distance in X & Y
	var distX=10;  // cutter bar width
	var distY=10;	// can be more than cutterbar width, this is segment in vertical distane
	//var steps=23;
	var stepsX=23;
	var stepsY=13;

	stepsX=9;  // if it is more the diplay is blank !
	pStart.x = p.x;
	pStart.y = p.y;
for (j=0; j<stepsX; j++){

// (1) ### move   Down
		while (isInside(...poly)){  // Is the 'p' point inside the polygon ?
			p.y=p.y + distY;
			totalDist = totalDist + distY;
		}
		p.y=p.y - distY;
		totalDist = totalDist - distY;
		
		//ctx.moveTo(p.x, pStart.y);  // make a line form begining till the last inside point
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=pStart.y;
		tpCounter++;
		
		//ctx.lineTo(p.x, p.y); 	// Make the first long line		//### STORE IN 'pixelTraceCoord' array
		//ctx.stroke()
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;		

// (2) ### 1st turing point:
		nrOfTurns ++;
		p.x=p.x + distX;		
		totalDist = totalDist + distX;
		
		p.y=p.y + 20*distY;   //sweep from left ot right
		while (!isInside(...poly)){
			p.y=p.y - distY;
		}
		//ctx.lineTo(p.x, p.y);					//### STORE IN 'pixelTraceCoord' array
		//ctx.stroke();
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		
		pStart.y = p.y;   // Store the starting poinn for retur line
	
// (3) ### move Up
		while (isInside(...poly)){  
			p.y=p.y - distY;
			totalDist = totalDist + distY;
		}
		p.y=p.y + distY;
		totalDist = totalDist - distY;

		//ctx.moveTo(p.x, pStart.y); 
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=pStart.y;
		tpCounter++;
		
		//ctx.lineTo(p.x, p.y);		// Make the second long line		//### STORE IN 'pixelTraceCoord' array	
		//ctx.stroke();
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;

// (4) ### 2nd turing point:
		nrOfTurns ++;
		p.x=p.x + distX;			
		totalDist = totalDist + distX;
		
		p.y=p.y - 3*distY;   //sweep from left ot right
		while (!isInside(...poly)){
			p.y=p.y + distY;
		}

		//ctx.lineTo(p.x, p.y);		//### STORE IN 'pixelTraceCoord' array
		//ctx.stroke();
		tp[tpCounter][0]=p.x; tp[tpCounter][1]=p.y;
		tpCounter++;
		pStart.y = p.y;
		
} // Y

}  // End Vertical


//***************************************************
// ********* Put the final line on the map **********
var tpGeo = new Array(tpCounter);  // Array for turning points in geo-coordinates 

for (i=0; i<tpCounter; i++)
	tpGeo[i]=map.layerPointToLatLng([tp[i][0],tp[i][1]]);

tarcePolyline = new L.Polyline(tpGeo, {  
		color: 'red',
		weight: 3,
		opacity: 0.5,
		smoothFactor: 1
	});
	tarcePolyline.addTo(map);	// *** draw the full section

}  // End of function
