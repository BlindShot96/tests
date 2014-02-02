package com.example.SimpleTestClient.Internet;

import android.R;
import android.util.Log;

import java.io.*;
import java.net.InetAddress;
import java.net.Socket;
import java.util.ArrayList;

/**
 * Project: SimpleTestClient
 * Author: Zaydel Peter
 * Date: 08.06.13
 * Time: 18:31
 */
public class TcpClient {



    public interface LoadProcentHandler
    {
        public void OnLoad(int all_size, int downloaded);
    }

    public LoadProcentHandler loadProcentHandler;

    /**
     * максимально допустимая длина сообщения
     */
    public final int MAX_SEND_LENGTH = 512;

    public final String END_DATA = "#@END@#";

    /**
     * сообщение  сервера
     */
    private String serverMessage;

    /**
     * адрес сервера
     */
    public  String ServerIp; //your computer IP addo  /**
//     * порт сервера
//     */
    public  int ServerPort;

    /**
     * событие - принято сообщение
     */
    private OnMessageReceived mMessageListener = null;

    private boolean mRun = false;

    PrintWriter out;
    BufferedReader in;
    Socket socket;

    /**
     *  Constructor of the class. OnMessagedReceived listens for the messages received from server
     */
    public TcpClient(OnMessageReceived listener) {
        mMessageListener = listener;
    }

    public TcpClient() {
    }

    /**
     * Sends the message entered by client to the server
     * @param message text entered by client
     */
    private void sendMessage(String message){
        if (out != null && !out.checkError()) {
            out.println(message);
            out.flush();
        }
    }

    /**
     * Остановка всего и вся))
     * @throws java.io.IOException
     */
    public void Stop(){
        mRun = false;
        if(this.socket != null){
            try {
                this.socket.close();
            } catch (IOException e) {
                this.socket = null;
            }
        }

        if(this.in != null)
        {
            try {
                this.in.close();
            } catch (IOException e) {
                this.in = null;
            }
        }

        if(this.out != null)
        {
            this.out.close();
        }
    }

    /**
     * подключение к серверу
     * @param ip  Ip адрес сервера
     * @param Port  порт сервера
     * @throws java.io.IOException
     */
    public void  Connect(String ip, int Port) throws IOException {
        this.ServerIp = ip;
        this.ServerPort = Port;
        Log.e("TCP Client", "C: Connecting...");
        try{
           socket = new Socket(this.ServerIp,this.ServerPort);
        }
        catch (Exception ex)
        {
            throw new IOException();
        }
        Log.e("TCP Client", "C: Connected successfully");
    }

    /**
     * отправит заранее подключённому серверу сообщение
     * @param s  сообщение
     * @throws Exception
     */
    public void Send(String s) throws Exception {
        if(this.socket == null){throw new  IllegalArgumentException("Нет подключения с сервером");}

        int message_length = s.length();
       // String msg = "*" + message_length + "*" + s;
        this.socket.setSendBufferSize(MAX_SEND_LENGTH);
        if (socket.getOutputStream() != null){
            for (byte[] mas : divide_bytes_mas(s.getBytes()))
            {
                socket.getOutputStream().write(mas);
            }
            socket.getOutputStream().flush();
        }
        else
        {
            throw new IOException("Не удалось отослать сообщение");
        }
    }

    private ArrayList<byte[]> divide_bytes_mas(byte[] bytes)
    {
        int max= MAX_SEND_LENGTH;//максимальное кол-во байт для одного пакета

        ArrayList<byte[]> list_bytes = new ArrayList<byte[]>();
        int k = bytes.length/max;
        int all_l = 0;
        for (int i =0; i< k; i++)
        {
            byte[] mas = new byte[max];
            System.arraycopy(bytes,i*max,mas,0,max);
            list_bytes.add(mas);
            all_l+=max;
        }
        if(all_l<bytes.length)
        {
            byte[] mas = new byte[bytes.length-all_l];
            System.arraycopy(bytes,all_l,mas,0,bytes.length-all_l);
            list_bytes.add(mas);
        }

        list_bytes.add(END_DATA.getBytes());

        return list_bytes;
    }


    public String NewRecieve() throws IOException {

        ArrayList<byte[]> buf = new ArrayList<byte[]>();

        int read = -1;

        byte[] END_DATA = "#@END@#".getBytes();

        while (true)
        {
            byte[] mas = new byte[MAX_SEND_LENGTH];
            read = this.socket.getInputStream().read(mas, 0, mas.length);

            buf.add(mas);

            String str1 = new String(mas,"UTF-8");
            String str2 =new String(END_DATA,"UTF-8");
            if (str1.indexOf(str2) > -1 || read == 0)
            {
                break;
            }
        }

        byte[] result = new byte[MAX_SEND_LENGTH*buf.size()];
        int position = 0;
        for (int i = 0; i < buf.size(); i++)
        {
            System.arraycopy(buf.get(i), 0, result, position, MAX_SEND_LENGTH);
            position += MAX_SEND_LENGTH;
        }

        String msg = new String(result,"UTF-8");

        return (msg.replaceAll(new String(END_DATA),"")).replaceAll("\0","");

    }

    /**
     * приём одного сообщения сервера
     * @return сообщение
     * @throws java.io.IOException
     */
    public String Recieve() throws IOException {
        if(this.socket == null){throw new  IllegalArgumentException("Нет подключения с сервером");}

        this.socket.setReceiveBufferSize(1000000);
        this.in = new BufferedReader(new InputStreamReader(this.socket.getInputStream()));
        this.mRun = true;

        Log.e("SERVER", "C: Start receiving ");

        int size=0;
        int downloaded=0;
        int string_length = 0;

        do {
            try{
                String s = in.readLine();
                if(s == null)
                {
                    mRun = false;
                    break;
                }
                if(s.toCharArray()[0] == '*')
                {
                  String size_s = s.substring(1);
                  size = Integer.parseInt(size_s);
                }
                else
                {
                  serverMessage += s + "\n";
                  string_length = s.getBytes().length;
                  downloaded+=string_length;
                    if(this.loadProcentHandler != null)
                    {
                        this.loadProcentHandler.OnLoad(size,downloaded);
                    }
                }
            }catch (Exception ex)
            {
                break;
            }
            if(mRun == false)
            {
                break;
            }
        }while (mRun == true || serverMessage == null);

        if (serverMessage != null && mMessageListener != null) {
            //call the method messageReceived from MyActivity class
            mMessageListener.messageReceived(serverMessage);
        }

        Log.e("SERVER", "S: Received Message: '" + serverMessage + "'");

        return  serverMessage.substring(3,serverMessage.length());
    }

    /**
     * СТАРАЯ ВЕРСИЯ НЕ ИСПОЛЬЗОВАТЬ
     */
    private void OLD_run() {

        mRun = true;

        try {
            //here you must put your computer's IP address.
            InetAddress serverAddr = InetAddress.getByName(ServerIp);

            Log.e("TCP Client", "C: Connecting...");

            //create a socket to make the connection with the server
            Socket socket = new Socket(serverAddr, ServerPort);

            try {

                //send the message to the server
                out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);

                Log.e("TCP Client", "C: Sent.");

                Log.e("TCP Client", "C: Done.");

                //receive the message which the server sends back
                in = new BufferedReader(new InputStreamReader(socket.getInputStream()));

                //in this while the client listens for the messages sent by the server
                while (mRun) {
                    serverMessage = in.readLine();

                    if (serverMessage != null && mMessageListener != null) {
                        //call the method messageReceived from MyActivity class
                        mMessageListener.messageReceived(serverMessage);
                    }
                    serverMessage = null;

                }


                Log.e("RESPONSE FROM SERVER", "S: Received Message: '" + serverMessage + "'");


            } catch (Exception e) {

                Log.e("TCP", "S: Error", e);

            } finally {
                //the socket must be closed. It is not possible to reconnect to this socket
                // after it is closed, which means a new socket instance has to be created.
                socket.close();
            }

        } catch (Exception e) {

            Log.e("TCP", "C: Error", e);

        }

    }

    //Declare the interface. The method messageReceived(String message) will must be implemented in the MyActivity
    //class at on asynckTask doInBackground
    public interface OnMessageReceived {
        public void messageReceived(String message);
    }
}
