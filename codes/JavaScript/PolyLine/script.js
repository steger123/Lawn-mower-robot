
const map = L.map('fieldMap').setView([28.4588446, 77.2867589], 17);   //The initial point
var layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
map.addLayer(layer); // Adding layer to the map

var marker_arr = [];
var marker_pos = [];
var marker_new = [];
///////////////////
// ADD markers :
//var a = new L.LatLng(28.4588446, 77.2867589),
//    b = new L.LatLng(28.4578446, 77.2867589),
//    c = new L.LatLng(28.45788446, 77.2877589);

marker_pos[0] = new L.LatLng(28.4588446, 77.2867589);
marker_pos[1] = new L.LatLng(28.4578446, 77.2867589);
marker_pos[2] = new L.LatLng(28.45788446, 77.2877589);


var locationIcon = L.icon({
    iconUrl: 'location.png',
    iconSize: [32, 32],
    iconAnchor: [16, 16]
    //  popupAnchor: [-3, -76],
    //  shadowUrl: 'my-icon-shadow.png',
    //  shadowSize: [68, 95],
    //  shadowAnchor: [22, 94]
});
var tractorIcon = L.icon({
    iconUrl: 'tractor.png',
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
marker_arr[0] = new L.Marker(marker_pos[0], { icon: tractorIcon, draggable: true }).addTo(map);
marker_arr[1] = new L.Marker(marker_pos[1], { icon: locationIcon, draggable: true }).addTo(map);
marker_arr[2] = new L.Marker(marker_pos[2], { icon: locationIcon, draggable: true }).addTo(map);

var polyline_demo = new L.Polyline([marker_pos[0], marker_pos[1], marker_pos[2]]).addTo(map);  // make the polyline as well

var myGroup = L.layerGroup([marker_arr[0], marker_arr[1], marker_arr[2]]);
myGroup.eachLayer(function (layer) {
    layer.setOpacity(0.5);
});

//L.marker([50.505, 30.57], { icon: myIcon }).addTo(map);
//marker = L.marker([28.4598446,77.2867589], { icon: tractorIcon, draggable: true }).addTo(map).on('click', onTractor);


// Now on dragstart you'll need to find the latlng from the polyline which corresponds
// with your marker's latlng and store it's key in your marker instance so you can use it later on:
function dragStartHandler(e) {
    var latlngPoly = polyline_demo.getLatLngs(),		// Get the polyline's latlngs
        latlngMarker = this.getLatLng();		// Get the actual, cliked MARKER's start latlng
    console.log("start");
    for (var i = 0; i < latlngPoly.length; i++) {		// Iterate the polyline's latlngs
        if (latlngMarker.equals(latlngPoly[i])) {		// Compare marker's latlng ot the each polylines 
            this.polylineLatlng = i;			// If equals store key in marker instance
        }
    }
}

// Now you know the key of the polyline's latlng you can change it
// when dragging the marker on the dragevent:
function dragHandler(e) {
    var latlngPoly = polyline_demo.getLatLngs(),	// Get the polyline's latlngs
        latlngMarker = this.getLatLng();			// Get the marker's current latlng
    console.log("drag");
    latlngPoly.splice(this.polylineLatlng, 1, latlngMarker);		// Replace the old latlng with the new
    polyline_demo.setLatLngs(latlngPoly);			// Update the polyline with the new latlngs
}

// Just to be clean and tidy remove the stored key on dragend:
function dragEndHandler(e) {
    delete this.polylineLatlng;
    console.log("end");
}

//You'll need to attach eventlisteners and callbacks to your L.Marker's.
// You could automate this, but i'll keep it simple for now:
// https://stackoverflow.com/questions/33513404/leaflet-how-to-match-marker-and-polyline-on-drag-and-drop

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
var lineCount = 0;
// make a new 2D polyline array
var newPoly = new Array(20);
// Loop to create 2D array using 1D array 
for (var i = 0; i < newPoly.length; i++) {
    newPoly[i] = new Array(2);
}


function btnRouting() {
    var property = document.getElementById("myRouting");
    if (doRouting) {
        doRouting = false;
        property.style.backgroundColor = "#ecebeb"
        console.log('Line count: ');
        console.log(lineCount);
        //make one polyline from clickings, which shall me draggable later
        //var polyline2 = new L.Polyline(pp, { color: 'red' }).addTo(map);

        var newPoly2 = new Array(lineCount + 1);  // this is required, becuse newPoly have empty values, which is not accepted by L.Polyline
        // Loop to create 2D array using 1D array 
        for (var i = 0; i < newPoly2.length; i++) {
            newPoly2[i] = new Array(2);
            newPoly2[i][0] = newPoly[i][0];
            newPoly2[i][1] = newPoly[i][1];

        }
        console.log(newPoly);
        console.log(newPoly2);
        console.log('orange');
        //make one polyline from clickings, which shall me draggable later
        var polyline3 = new L.Polyline(newPoly2, { color: '#00AA00', weight: 10, opacity: 0.4 }).addTo(map);

        //make one polyline from clickings, which shall me draggable later
        // var polyline2 = new L.Polyline(pp, { color: 'red' }).addTo(map);

    } else {   //End the routing
        doRouting = true;
        property.style.backgroundColor = "#7FFF00"

    }
}

map.on("click", function (e) {  //Listener: Click on MAP -> Addign the WAYPOINTS
    if (doRouting) {
        var newLine = [
            startPoint,
            [e.latlng.lat, e.latlng.lng]  //mouse click position in lat & long
        ];
        //arr[lineCount][0] = startPoint[0];
        //arr[lineCount][1] = startPoint[1];
        //arr[lineCount][2] = e.latlng.lat;
        //arr[lineCount][3] = e.latlng.lng;
        newPoly[lineCount][0] = startPoint[0];
        newPoly[lineCount][1] = startPoint[1];
        newPoly[lineCount + 1][0] = e.latlng.lat;
        newPoly[lineCount + 1][1] = e.latlng.lng;

        lineCount++;
        // initial line & marker:
        new L.polyline(newLine, { color: 'blue', noClip: true }).addTo(map);
        //L.circleMarker([e.latlng.lat, e.latlng.lng]).addTo(map);  //move the Waypoint
        marker_new[lineCount] = new L.marker([e.latlng.lat, e.latlng.lng], { icon: locationIcon, draggable: true, opacity: 0.4 }).addTo(map).on('dragstart', dragStartHandler).on('drag', dragHandler).on('dragend', dragEndHandler);


        startPoint = [e.latlng.lat, e.latlng.lng];
        console.log(arr);
    }
    else {

        // console.log(L.circleMarker.getLatLng()[0]);   //detect which waypoint is slected
    }
})
