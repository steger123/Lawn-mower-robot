using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Map_v1
{
    class MySerialPortClass
    {

        /*
    public SerialPort SerialPort { get; private set; }

  	const int DefualtBaudRate = 6800;
  	const int DefaultSize = 8;
  	const Parity DefaultParityBit = Parity.None;
	const StopBits DefaultStopBits= StopBits.One;
 
  	public bool Open = false ;

	private bool Disposed = false;
    MySerialPortClass(string ComPort, int BaudRateValue = DefualtBaudRate, Parity Par = DefaultParityBit, int DataSize = DefaultSize, StopBits Stop = DefaultStopBits)
    {
        SerialPort = new SerialPort(ComPort, BaudRateValue, Par, DataSize, Stop);
    }
  
    void OpenPort()
    {
    	Open = true;
      	SerialPort.Open();
    }
     
	//reading and writeing is simple after set up and opening, but for each 
	//device messages will have to be formated differntly. Check with your
	//devices manual or data sheets for more on formatting. 
  	//can read a single byte at a time for decoding messages as they come in
	byte Readbyte()
    {
      	return (byte)SerialPort.ReadByte();//cast is because dispite it being a byte ms made it an int container
    }
  
  	char ReadChar()
    {
      	return (char)SerialPort.ReadByte();//cast is because dispite it being a byte ms made it an int container
    }
	
  	//or you can read a string out if you know messages are on a line or 
  	//would rather mass decode
  	//to read all in the buffer into a string
  	string ReadExisting()
    {
      	return SerialPort.ReadExisting();
    }
  	//if you know messages are a line like in a doc
  	string ReadExisting()
    {
      	return SerialPort.ReadLine();
    }
  	//Lastly you can decode as a buffer if you know message lengths
  	//Not it will fill in the buffer you provide and not size.
  	public int Read(byte[] buffer, int offset, int count);
  	{
      	//return SerialPort.Read(buffer, offset, count);
  	}
	//You can simply write a sting out
  	public void Write(string text)
    {
      	return SerialPort.Write(text);
    }
  	//Or you can write from a buffer like in a stream
  	public int Write(byte[] buffer, int offset, int count);
  	{
      	return SerialPort.Write(buffer, offset, count);
  	}
	//Or you can write a line from a string
	public void WriteLine(string text)
    {
      	return SerialPort.WriteLine(text);
    }
  
  	//Lastly it is recomended thay you dispose of your class and free system
	//Resorces
    //Close will free the port for use by a different source
    void Close()
    {
    	Open = false;
    	return SerialPort.Close();
    }
    //allows a using statement to dispose after use elegantly
	public void Dispose()
   	{
    	Disposed = true;
        SerialPort.Close();
      	SerialPort.Dispose();
   	}
    //in the garbage collection ensure disposal so port will open back up after.
    ~MySerialPortClass()
    {
    	if(!Disposed)
        {
      		Dispose();
        }
    }

        */
    }  // end class
}  // end namespace
