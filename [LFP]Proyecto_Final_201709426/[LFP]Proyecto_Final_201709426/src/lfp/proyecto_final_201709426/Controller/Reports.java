/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package lfp.proyecto_final_201709426.Controller;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import javax.swing.JFileChooser;
import javax.swing.JOptionPane;
import javax.swing.filechooser.FileFilter;
import javax.swing.filechooser.FileNameExtensionFilter;
import lfp.proyecto_final_201709426.Model.Errror;
import lfp.proyecto_final_201709426.Model.Token;

/**
 *
 * @author Samuel
 */
public class Reports {

    ArrayList<Token> lista_token = new ArrayList();
    ArrayList<Token> auxiliar = new ArrayList();
    
    ArrayList<Errror> errores_sintacticos = new ArrayList();
    ArrayList<Errror> errores_lexicos = new ArrayList();

    public Reports(ArrayList<Token> lista_token,
            ArrayList<Errror> errores_lexicos,ArrayList<Errror> errores_sintacticos ) {
        this.lista_token = lista_token;
        this.errores_lexicos = errores_lexicos;
        this.errores_sintacticos = errores_sintacticos;
    }
    
    public void crear_reporte(){
        elimiar_redundacia();
        String html = "";
        html = "<html>"
                + "<body>"
                + "<h1>ERRORES LEXICOS ENCONTRADOS EN EL ANALIS</h1>"
                + " <table border=\"1\"  style=\"margin: 0 auto;\"> <tr>"
                + "<th>Lexema</th>             "
                + "<th>Fila</th>"
                + "<th>Columna</th>"
                + "<th>Descripcion</th></tr>";
                
        for (int i = 0; i < errores_lexicos.size(); i++) {
        html = html + "<tr><th>" + errores_lexicos.get(i).getLexema() + "</th>"
                + "<th>" + errores_lexicos.get(i).getFila() + "</th>"
                + "<th>" + errores_lexicos.get(i).getColumna()+ "</th>"
                + "<th>" + errores_lexicos.get(i).getDescripcion()+ "</th></tr>";
    
        }
          html = html + "</table><br/><br/><h1>ERRORES SINTACTICOS ENCONTRADOS EN EL ANALIS</h1>"
                + " <table border=\"1\"  style=\"margin: 0 auto;\"> <tr>"
                + "<th>Lexema</th>             "
                + "<th>Fila</th>"
                + "<th>Columna</th>"
                + "<th>Descripcion</th></tr>";
                
        for (int i = 0; i < errores_sintacticos.size(); i++) {
        html = html + "<tr><th>" + errores_sintacticos.get(i).getLexema() + "</th>"
                + "<th>" + errores_sintacticos.get(i).getFila() + "</th>"
                + "<th>" + errores_sintacticos.get(i).getColumna()+ "</th>"
                + "<th>" + errores_sintacticos.get(i).getDescripcion()+ "</th></tr>";
    
        }
       html = html + "</table><br/><br/><h1>TABLA DE TOKENS RECONCIDOS</h1>"
                + " <table border=\"1\"  style=\"margin: 0 auto;\"> <tr>"
                + "<th>Lexema</th>"
                + "<th>Numero de Token</th>"
                + "<th>Fila</th>"
                + "<th>Columna</th>"
                + "<th>Tipo</th></tr>";
                
        for (int i = 0; i < auxiliar.size(); i++) {
        html = html + "<tr><th>" + auxiliar.get(i).getLexema() + "</th>"
                + "<th>" + auxiliar.get(i).getNumero_token()+ "</th>"
                + "<th>" + auxiliar.get(i).getFila() + "</th>"
                + "<th>" + auxiliar.get(i).getColumna()+ "</th>"
                + "<th>" + auxiliar.get(i).getTipo()+ "</th></tr>";
        }
        html =html + "</table></body></html>";
        String ruta = "C:\\Users\\Samuel\\Desktop\\LFP\\[LFP]Proyecto_Final_201709426\\analisis.html";
        File archivo = new File(ruta);
        archivo.delete();
        archivo = new File(ruta);
        FileWriter escribir;
        try {

            escribir = new FileWriter(archivo, true);
            escribir.write(html);
            escribir.close();

        } catch (FileNotFoundException ex) {
            JOptionPane.showMessageDialog(null, "Error al guardar, ponga nombre al archivo");
        } catch (IOException ex) {
            JOptionPane.showMessageDialog(null, "Error al guardar, en la salida");
        }
        try{
            ProcessBuilder pb = new ProcessBuilder();
            pb.command("cmd.exe", "/c", ruta);
            pb.start();
        }catch(IOException e){
        }
    }
    public void elimiar_redundacia(){
        for (int i = 0; i < lista_token.size(); i++) {
            if (auxiliar.isEmpty()) {
                auxiliar.add(lista_token.get(i));
            }
            if (buscar(lista_token.get(i).getLexema()) == false) {
                auxiliar.add(lista_token.get(i));
            }
        }
    }
    public boolean buscar(String lexema){
       
        for (int i = 0; i < auxiliar.size(); i++) {
            if (auxiliar.get(i).getLexema().equals(lexema)) {
                return true;
            }
        }
        return false;
    }
}
