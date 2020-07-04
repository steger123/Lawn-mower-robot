
function removeActiveTab() {
  var actives = document.getElementsByClassName("nav-link active");
  for (var l = 0; l < actives.length; l++) {
    actives[l].className = "nav-link";
  }
}

// Switch to flight data tab
function flightDataTab() {
  // Change active tab
  removeActiveTab();
  document.getElementById("flight-data-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "visible";
  document.getElementById("mission-plan-panel").style.display = "none";
  document.getElementById("geofence-panel").style.display = "none";
  document.getElementById("import-export-panel").style.display = "none";
  document.getElementById("camera-panel").style.display = "none";
  /*
    // Disable mission planner
    disable_mission_planner();
    disable_geofence_planner();
  
    // Draw drone on next location
    drawDrone = true;
    */
}



// Switch to plan mission tab
function planMissionTab() {

  // Change active tab
  removeActiveTab();
  document.getElementById("mission-plan-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "hidden";
  document.getElementById("mission-plan-panel").style.display = "block";
  document.getElementById("geofence-panel").style.display = "none";
  document.getElementById("import-export-panel").style.display = "none";
  document.getElementById("camera-panel").style.display = "none";
  /*
    if (missionRunning == false) {
  
      if (missionSet == false) {
        // Enable mission planner
        enable_mission_planner();
      }
      disable_geofence_planner();
  
      // Draw home marker
      draw_home_marker();
    }
  
    show_mission();
    show_geofence();
  
    resizeMissionTable();
    */
}

// Switch to geofence tab
function geofenceTab() {
  // Change active tab
  removeActiveTab();
  document.getElementById("flight-area-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "hidden";
  document.getElementById("mission-plan-panel").style.display = "none";
  document.getElementById("geofence-panel").style.display = "block";
  document.getElementById("import-export-panel").style.display = "none";
  document.getElementById("camera-panel").style.display = "none";
  /*
    // Disable mission planner
    disable_mission_planner();
  
    // Enable flight area click recognizer
    if (geofenceSet == false && missionRunning == false) {
        enable_geofence_planner();
      }
    
    //show_mission();
    //show_geofence();
    
      if (missionRunning == false) {
        // Draw home marker
        draw_home_marker();
      }
    
      resizeGeofenceTable();
  */
}

// Switch to import export tab
function importExportTab() {
  // Change active tab
  removeActiveTab();
  document.getElementById("import-export-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "hidden";
  document.getElementById("mission-plan-panel").style.display = "none";
  document.getElementById("geofence-panel").style.display = "none";
  document.getElementById("import-export-panel").style.display = "block";
  document.getElementById("camera-panel").style.display = "none";

  // Enable export mission button only if mission is set
  /* 
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
   
    // disable_mission_planner();
    // disable_geofence_planner();
  */

}

// Switch to video tab
function videoTab() {
  // Change active tab
  removeActiveTab();
  document.getElementById("camera-tab").className = "nav-link active";

  // Change panel visibility
  document.getElementById("flight-data-panel").style.visibility = "hidden";
  document.getElementById("mission-plan-panel").style.display = "none";
  document.getElementById("geofence-panel").style.display = "none";
  document.getElementById("import-export-panel").style.display = "none";
  document.getElementById("camera-panel").style.display = "block";
}
