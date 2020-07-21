
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


var
  _firstLatLng,
  _firstPoint,
  _secondLatLng,
  _secondPoint,
  _distance,
  _length,
  _polyline;
  



function btnMeasure() {
//	'leaflet distance to example'
// https://embed.plnkr.co/fmV4B2XC0c5cnlQn6Cq9/
//https://leafletjs.com/reference-1.4.0.html#latlng
// https://stackoverflow.com/questions/43167417/calculate-distance-between-two-points-in-leaflet


 map.on('click', function(e) {

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
  }
})

map.on('zoomend', function(e) {
  refreshDistanceAndLength();
})

  
}  //end btnMeasure

function refreshDistanceAndLength() {
   _length = getDistance(_firstLatLng, _secondLatLng);
  document.getElementById('length').innerHTML = _length.toFixed(2);
}

//=======================
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



function btnAutoRoute() {
//https://stackoverflow.com/questions/30653504/how-to-draw-rectangle-marker-in-leaflet-given-only-1-lat-lon-pair

	
// **** make the rectangle from pixelpoints ****

//var latLng = L.latLng(28.4588446, 77.2867589); latLng_Zero : see in script.js
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
	
//	tarcePolyline[0] = new L.Polyline(pointList, {
//		color: 'red',
//		weight: 3,
//		opacity: 0.5,
//		smoothFactor: 1
//	});
//	tarcePolyline[0].addTo(map);

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
	var p2g = map.layerPointToLatLng(p2);
	
	var p3 = L.point((currentPoint.x + d*(i*2+1) + ox), (currentPoint.y + h + oy));
	var p3g = map.layerPointToLatLng(p3);
	
	var p4 = L.point((currentPoint.x + d*(i*2+1) + ox), (currentPoint.y) + oy);
	var p4g = map.layerPointToLatLng(p4);
	
	var p5 = L.point((currentPoint.x + d*(i*2+2) + ox), (currentPoint.y) + oy);
	var p5g = map.layerPointToLatLng(p5);
	
	pointList = [p1g, p2g, p3g, p4g, p5g];
	
	tarcePolyline[i] = new L.Polyline(pointList, {
		color: 'red',
		weight: 3,
		opacity: 0.5,
		smoothFactor: 1
	});
	tarcePolyline[i].addTo(map);
}	
// ctx.lineTo(w+ox, h+oy);

/*
 
	//ctx.moveTo(0, 0);
	//ctx.lineTo(0, h);
	//ctx.lineTo(d, h);
	//ctx.lineTo(d, 0);
	//ctx.lineTo(d*(i+1),0);
	
	//ctx.moveTo(0, 0);
	//ctx.lineTo(0, h);  //1st line
	//ctx.lineTo(d*(i+1), h);  //2nd line
	//ctx.lineTo(d*(i+1), 0);   //3rd line
	//ctx.lineTo(d*(i+2), 0);
		
	//ctx.moveTo(2*d*i, 0);
	//ctx.lineTo(2*d*i, h);  //1st line
	//ctx.lineTo(d*(i*2+1), h);  //2nd line
	//ctx.lineTo(d*(i*2+1), 0);   //3rd line
	//ctx.lineTo(d*(i*2+2), 0)

	ctx.moveTo(ox+2*d*i, oy+0);  // modify the line inward with offsets in x & y direction
	ctx.lineTo(ox+2*d*i, h+oy);  //1st line
	ctx.lineTo(ox+d*(i*2+1), h+oy);  //2nd line
	ctx.lineTo(ox+d*(i*2+1), 0+oy);   //3rd line
	ctx.lineTo(ox+d*(i*2+2), 0+oy)

	
}  // end for loop
ctx.lineTo(w+ox, h+oy);

*/

}

//var areaSelected = false;  // if I click on the map then rectangle made ], but poitn sonnected as well
							// due to clic listener in script.js

function btnArea() {
	//areaSelected= true;
	map.on('click', function(e) {

	if (!_firstLatLng) {
		_firstLatLng = e.latlng;
		//L.marker(_firstLatLng).addTo(map).bindPopup('Point A<br/>' + e.latlng).openPopup();
		L.marker(_firstLatLng, {draggable: true}).addTo(map)
		//marker_new[lineCount] = new L.marker([e.latlng.lat, e.latlng.lng], { icon: locationIcon, draggable: true, opacity: 0.4 }).addTo(map).on('dragstart', dragStartHandler).on('drag', dragHandler).on('dragend', dragEndHandler);
        //marker_new[lineCount].parentLine = tempLine;
		
		
	} else {
		_secondLatLng = e.latlng;
		//L.marker(_secondLatLng).addTo(map).bindPopup('Point B<br/>' + e.latlng).openPopup();
		L.marker(_secondLatLng, {draggable: true}).addTo(map);
	}

	if (_firstLatLng && _secondLatLng) {
		// draw the line between points
		L.rectangle([_firstLatLng, _secondLatLng], {
		color: 'green'
		}).addTo(map);
	}
	})
	//areaSelected= false;
}

