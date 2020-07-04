const map = L.map('fieldMap').setView([28.4588446, 77.2867589], 17);   //The initial point
var layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
map.addLayer(layer); // Adding layer to the map

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
marker_arr[0].parentLine = polyline_demo;
marker_arr[1].parentLine = polyline_demo;
marker_arr[2].parentLine = polyline_demo;

// chanhe the TRACTOR markers opacity in one shot:
var myGroup = L.layerGroup([marker_arr[0], marker_arr[1], marker_arr[2]]);
myGroup.eachLayer(function (layer) {
    layer.setOpacity(0.5);
});

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
                this.polylineLatlng = i;            // If equals store key in marker instance

                wayPoints[i][0] = latlngPoly[i].lat;  // sotre the dragged, new coordinates in the matrix
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
var lineCount = 0;

// make a new 2D matrix
var wayPoints = new Array(100);
// Loop to create 2D matrix using 1D matrix 
for (var i = 0; i < wayPoints.length; i++) {
    wayPoints[i] = new Array(2);
}


function btnRouting() {
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

    if (doRouting) {
        doRouting = false;
        //  property.style.backgroundColor = "#ecebeb"
        console.log('Line count: ');
        console.log(lineCount);

        //make one polyline from clickings, which shall be draggable later:
        // ****** THIS IS NOT MOVING IF I CHANGE it in the HANDLERS *******//

        var polyline = tempLine.setStyle({ color: '#00AA00', weight: 10, opacity: 0.4 });
        tempLine = null;

        //*****************************************************************//


    } else {   //End the routing
        doRouting = true;
        // property.style.backgroundColor = "#7FFF00"

    }
}


map.on("click", function (e) {  //Listener: Click on MAP -> Addign the WAYPOINTS
    if (doRouting) {
        if (!tempLine) {
            tempLine = L.polyline([], { color: 'blue', noClip: true }).addTo(map);
        }
        tempLine.addLatLng(e.latlng);

        addRow('dataTable', startPoint[0], startPoint[1]); // add one row on the panel
        // store bule line coordinates for later comutation (make green line, table etc.)
        wayPoints[lineCount][0] = startPoint[0];
        wayPoints[lineCount][1] = startPoint[1];
        // newPoly[lineCount + 1][0] = e.latlng.lat;  // new click point
        // newPoly[lineCount + 1][1] = e.latlng.lng;

        lineCount++;
        //L.circleMarker([e.latlng.lat, e.latlng.lng]).addTo(map);  //move the Waypoint
        // add marker on the map
        marker_new[lineCount] = new L.marker([e.latlng.lat, e.latlng.lng], { icon: locationIcon, draggable: true, opacity: 0.4 }).addTo(map).on('dragstart', dragStartHandler).on('drag', dragHandler).on('dragend', dragEndHandler);
        marker_new[lineCount].parentLine = tempLine;


        startPoint = [e.latlng.lat, e.latlng.lng];  // save the new points for the starter of the next point
        console.log(arr);
    }
    else {

        // console.log(L.circleMarker.getLatLng()[0]);   //detect which waypoint is selected
    }
})

function addRow(tableID, latit, longit) {

    var table = document.getElementById(tableID);

    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);

    var cell2 = row.insertCell(0);  //waypoint
    cell2.innerHTML = rowCount;

    var cell3 = row.insertCell(1);
    cell3.innerHTML = latit.toFixed(4);  // take only the last 4 digits

    var cell4 = row.insertCell(2);
    cell4.innerHTML = longit.toFixed(4);

    //document.getElementById(element4).innerText="ahoj";

}

function delete_row(tableID) {
    try {
        var table = document.getElementById(tableID);
        var rowCount = table.rows.length;
        if (rowCount > 1) {
            table.deleteRow(rowCount - 1);
        }
        //		for(var i=0; i<rowCount; i++) {
        //			var row = table.rows[i];
        //	var chkbox = row.cells[0].childNodes[0];
        //	if(null != chkbox && true == chkbox.checked) {
        //	table.deleteRow(i);
        //		rowCount--;
        //		i--;
        //	}
        //}
    } catch (e) {
        alert(e);
    }
}

function update_table(tableID) {
    // first delete the rowas expect the header
    // add new rown acc. 
    // newPoly[lineCount][0] = startPoint[0];
    // newPoly[lineCount][1] = startPoint[1];

    try {
        var table = document.getElementById(tableID);
        var rowCount = table.rows.length;  // this will decrease
        var totalRows = table.rows.length;
        for (var i = 1; i < rowCount; i++) {
            table.deleteRow(i);
            rowCount--;
            i--;
        }
    } catch (e) {
        alert(e);
    }


    var table = document.getElementById(tableID);
    //var rowCount = table.rows.length;
    for (var i = 1; i < totalRows; i++) {
        var row = table.insertRow(i);
        var cell2 = row.insertCell(0);  //waypoint
        cell2.innerHTML = i;
        var latit = wayPoints[i - 1][0];		//ontainer of the waipoint's polyline start &next is the end
        var longit = wayPoints[i - 1][1];

        var cell3 = row.insertCell(1);
        cell3.innerHTML = latit.toFixed(4);;

        var cell4 = row.insertCell(2);
        cell4.innerHTML = longit.toFixed(4);;


    }
}

function btnConnect() {
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



}
