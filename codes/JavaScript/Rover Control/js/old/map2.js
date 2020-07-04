
// Set map zoom at 600 meters
var altitude_view = 600;

// Set global variables
var wwd = null;
var placemarkLayer = new WorldWind.RenderableLayer("Placemarks");
var canvasBackground = null;
var missionClickRecognizer = null;
var missionTapRecognizer = null;
var geofenceClickRecognizer = null;
var geofenceTapRecognizer = null;
var coordinates = [];
var geofenceCoordinates = [];
var missionMarks = [];
var missionPaths = [];
var geofenceMarks = [];
var geofencePaths = [];
var geofenceShape = [];
var homeMark = null;
var droneMark = null;
var previousMark = null;
var previousGeoMark = null;
var altitude = null;
var groundAltitude = null;
var actionState = {};
var tabState = {};
var previousState = {};
var airplanes = {};
var bottomWrapper = 'button-wrapper';

// Set flags
var drawDrone = true;
var missionRunning = false;
var homeShowed = false;
var missionError = false;
var wait_for_active = true;
var missionHidden = null;
var geofenceHidden = null;
var geofenceSet = false;
var missionSet = false;
var mapLoaded = false;
var loadMapCalled = false;
var centerDrone = false;




// Enable mission click recognizer event listener
function enable_mission_planner() {
  if (missionClickRecognizer != null) {
    missionClickRecognizer.enabled = true;
    missionTapRecognizer.enabled = true;
  }
}

// Disable mission click recognizer event listener
function disable_mission_planner() {
  if (missionClickRecognizer != null) {
    missionClickRecognizer.enabled = false;
    missionTapRecognizer.enabled = false;
  }
}

// Enable flight area click recognizer event listener
function enable_geofence_planner() {
  if (geofenceClickRecognizer != null) {
    geofenceClickRecognizer.enabled = true;
    geofenceTapRecognizer.enabled = true;
  }
}

// Disable flight area click recognizer event listener
function disable_geofence_planner() {
  if (geofenceClickRecognizer != null) {
    geofenceClickRecognizer.enabled = false;
    geofenceTapRecognizer.enabled = false;
  }
}

// Disable all tabs
function disable_all_tabs() {

  var navs = document.getElementsByClassName("nav-item");
  for (var l = 0; l < navs.length; l++) {
    navs[l].className = "nav-item disabled";
  }

  // Save the previous state and disable onclick
  if (JSON.stringify(tabState) === JSON.stringify({})) {
    var navs = document.getElementsByClassName("nav-link");
    for (var l = 0; l < navs.length; l++) {
      tabState[navs[l].id] = navs[l].onclick;
      navs[l].onclick = null;
    }
  }
}









// Remove 'active' class from tabs
function removeActiveTab() {
  var actives = document.getElementsByClassName("nav-link active");
  for (var l = 0; l < actives.length; l++) {
    actives[l].className = "nav-link";
  }
}
// Show mission markers
function show_mission() {

  if (missionHidden == false) {
    return;
  }

  for (i = 0; i < missionMarks.length; i++) {
    placemarkLayer.addRenderable(missionMarks[i]);
  }

  for (i = 0; i < missionPaths.length; i++) {
    placemarkLayer.addRenderable(missionPaths[i]);
  }

  missionHidden = false;
  wwd.redraw();
}

// Hide mission markers
function hide_mission() {

  if (missionHidden == true) {
    return;
  }

  for (i = 0; i < missionMarks.length; i++) {
    placemarkLayer.removeRenderable(missionMarks[i]);
  }

  for (i = 0; i < missionPaths.length; i++) {
    placemarkLayer.removeRenderable(missionPaths[i]);
  }

  missionHidden = true;
  wwd.redraw();
}


// Switch to geofence tab
function geofenceTab() {

  // Change active tab
  removeActiveTab();
  document.getElementById("flight-area-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "hidden";
  document.getElementById("geofence-panel").style.display = "block";
  document.getElementById("mission-plan-panel").style.display = "none";
  document.getElementById("import-export-panel").style.display = "none";
  document.getElementById("camera-panel").style.display = "none";

  // Disable mission planner
  disable_mission_planner();

  // Enable flight area click recognizer
  if (geofenceSet == false && missionRunning == false) {
    enable_geofence_planner();
  }

  show_mission();
  show_geofence();

  if (missionRunning == false) {
    // Draw home marker
    draw_home_marker();
  }

  resizeGeofenceTable();
}


// Switch to import export tab
function importExportTab() {
  // Change active tab
  removeActiveTab();
  document.getElementById("import-export-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "hidden";
  document.getElementById("geofence-panel").style.display = "none";
  document.getElementById("mission-plan-panel").style.display = "none";
  document.getElementById("import-export-panel").style.display = "block";
  document.getElementById("camera-panel").style.display = "none";

  // Enable export mission button only if mission is set
  if (missionSet == false || missionError == true) {
    document.getElementById("export-mission").disabled = true;
  } else {
    document.getElementById("export-mission").disabled = false;
  }

  // Enable import mission button only if mission is not running
  if (missionRunning == true || missionError == true) {
    document.getElementById("import-mission").disabled = true;
  } else {
    document.getElementById("import-mission").disabled = false;
  }

  disable_mission_planner();
  disable_geofence_planner();
}


// Switch to video tab
function videoTab() {
  // Change active tab
  removeActiveTab();
  document.getElementById("camera-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "hidden";
  document.getElementById("geofence-panel").style.display = "none";
  document.getElementById("mission-plan-panel").style.display = "none";
  document.getElementById("import-export-panel").style.display = "none";
  document.getElementById("camera-panel").style.display = "block";
}
