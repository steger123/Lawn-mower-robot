let selectedPort;

let serial;
let latestData = "waiting for data";
let inData; // for incoming serial data
let options = {
	baudRate: 9600
};




function setup() {   // this automatically run when the file loaded
	//	createCanvas(windowWidth, windowHeight);
	serial = new p5.SerialPort();
	serial.list();
	//	serial.open('COM5');
	//	serial.on('connected', serverConnected);
	serial.on('list', gotList);     //get the list og the available ports
	serial.on('data', serialEvent);    //gotData
	serial.on('error', serialError);
	//	serial.on('open', selectedPort);  //gotOpen
	//	serial.on('close', gotClose);
	serial.clear();
}

function gotList(thelist) {    		//get the list and put it in a drop down list
	var select = document.getElementById("comPorts");
	print("List of Serial Ports:");

	for (let i = 0; i < thelist.length; i++) {
		print(i + " " + thelist[i]);

		var opt = thelist[i];
		var el = document.createElement("option");
		el.textContent = opt;
		el.value = opt;
		select.appendChild(el);  // add the port names one by one
	}
}

function serverConnected() {
	print("Connected to Server");
}

function openPort() {
	selectedPort = document.getElementById("comPorts").value;  //egt back with the slected port from the drop down list
	console.log(selectedPort);
	serial.open(selectedPort, options);  //OPne the selected Port Name
	serial.clear();
	print("Serial Port is Open");

}
//getSelectValue();

function closePort() {
	serial.close(selectedPort);
}

function gotOpen() {
	print("Serial Port is Open");
}

function gotClose() {
	print("Serial Port is Closed");
	inData = "Serial Port is Closed";
}

function gotError(theerror) {
	print(theerror);
}

function serialEvent() {    			//gotData
	// read a byte from the serial port:
	let inByte = serial.read();
	print("inByte: " + inByte);   // *** Print out the received bytes on he console. ***
	inData = inByte;             // save the data in global variable. (Can be deleted ?)
}

/*	function gotData() {   // keep for study purpose
		let currentString = serial.readLine();
		trim(currentString);
		if (!currentString) return;
		console.log(currentString);
		inData = currentString;
	}
*/
function draw() {
	//background(255, 255, 255);
	//fill(0, 0, 0);
	//text(latestData, 10, 10);

	document.getElementById("txt").innerHTML = inData;
	// Polling method
	/*
	if (serial.available() > 0) {
	 let data = serial.read();
	 ellipse(50,50,data,data);
	}
	*/
}

function keyTyped() {
	let outByte = key;   //send keyboard characters to Arduino
	console.log("Sending " + outByte);
	//serial.write(Number(outByte)); // Send as byte value
	serial.write(outByte); // Send as a string/char/ascii value
}

function serialError(err) {
	print('Something went wrong with the serial port. ' + err);
}

