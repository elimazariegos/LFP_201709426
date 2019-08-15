/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package lfp.proyecto_final_201709426.Controller;

import java.util.ArrayList;
import javax.swing.JOptionPane;
import javax.swing.JTextArea;
import lfp.proyecto_final_201709426.Model.Errror;
import lfp.proyecto_final_201709426.Model.Token;

/**
 *
 * @author Samuel
 */
public class Analisis_Lexico {

    ArrayList<Token> lista_token = new ArrayList();
    ArrayList<Errror> errores_lexicos = new ArrayList();

    public Analisis_Lexico(ArrayList<Token> lista_token, ArrayList<Errror> errores_lexicos) {
        this.lista_token = lista_token;
        this.errores_lexicos = errores_lexicos;
    }

    public void analizar(String texto) {
        int estado = 0;
        int decimal = 0;
        int numero_token = -1;
        texto = texto + "\n";
        String lexema = "";
        String tipo = "";
        String[] lineas = separador(texto, '\n');

        for (int i = 0; i < lineas.length; i++) {
            for (int j = 0; j < lineas[i].length(); j++) {
                int n_actual, n_siguiente = -1;

                n_actual = lineas[i].codePointAt(j);
                if (estado == 0) {
                    estado = estado_transicion(n_actual);
                }
                try {
                    n_siguiente = lineas[i].codePointAt(j + 1);
                } catch (Exception e) {
                }

                switch (estado) {
                    case 1:
                        lexema = lexema + lineas[i].charAt(j);
                        if ((n_siguiente > 96 && n_siguiente < 123) || (n_siguiente > 64 && n_siguiente < 91)
                                || (n_siguiente > 47 && n_siguiente < 58)) {
                            estado = 1;
                        } else {
                            numero_token = palabra_reservada(lexema);
                            tipo = "cadena";
                            if (numero_token != 1) {
                                tipo = "cadena-palabra reservada";
                            }

                            estado = 0;
                        }
                        break;
                    case 2:
                        lexema = lexema + lineas[i].charAt(j);
                        if ((n_siguiente > 47 && n_siguiente < 58) || (n_siguiente == 46 && decimal == 0)) {
                            if (n_siguiente == 46) {
                                decimal = 1;
                            }
                            estado = 2;
                        } else {
                            tipo = "numero";
                            numero_token = 2;
                            estado = 0;
                        }
                        break;
                    case 100:
                        estado = -2;
                        break;
                    case 101:
                        lexema = lexema + lineas[i].charAt(j);
                        errores_lexicos.add(new Errror("Error lexico", lexema, 101, null, i + 1, j + 1, "error"));
                        estado = -2;
                        lexema = "";
                        tipo = "";
                        break;
                    default:
                        lexema = lexema + lineas[i].charAt(j);
                        numero_token = estado;
                        estado = 0;

                        tipo = "Simbolo";
                        break;
                }
                if (estado == 0) {
                    lista_token.add(new Token(lexema, numero_token, null, i + 1, j + 1, tipo));
                    lexema = "";
                }
                if (estado == -2) {
                    estado = 0;
                }
            }
        }
        lista_token.add(new Token("#", 999, null, 0, 0, "#"));
    }

    public int estado_transicion(int numero) {

        if ((numero > 96 && numero < 123) || (numero > 64 && numero < 91)) {
            return 1;
        } else if ((numero > 47 && numero < 58)) {
            return 2;
        } else if (numero == 62) {// mayor q >
            return 3;
        } else if (numero == 60) { //menor q <
            return 4;
        } else if (numero == 44) {//coma ,
            return 5;
        } else if (numero == 47) {// diagonal /
            return 6;
        } else if (numero == 46) {// punto .
            return 7;
        } else if (numero == 39) { //comilla simple '
            return 8;
        } else if (numero == 33) {// admiracion !
            return 9;
        } else if (numero == 34) {// comilla doble
            return 10;
        } else if (numero == 36) {// Dolar
            return 11;
        } else if (numero == 63) {// interrogacion 
            return 12;
        } else if (numero == 32 || numero == 13 || numero == 9) {//espacios en blanco
            return 100;
        } else { // token no encontrado
            return 101;
        }
    }

        public int palabra_reservada(String lexema) {
        int numero = -1;
        switch (lexema) {
            case "Noticia":
                return 20;
            case "Farandula":
                return 21;
            case "Deportes":
                return 22;
            case "Publicidad":
                return 23;
            case "Titulo":
                return 24;
            case "Autor":
                return 25;
            case "Contenido":
                return 26;
            case "ImgUno":
                return 27;
            case "ImgDos":
                return 28;
            case "ImgTres":
                return 29;

            default:
                return 1;
        }
    }

    public String[] separador(String texto, char separar) {
        String linea = "";
        int contador = 0;
        for (int i = 0; i < texto.length(); i++) {
            if (texto.charAt(i) == separar) {
                contador++;
            }
        }
        String[] cadenas = new String[contador];
        contador = 0;
        for (int i = 0; i < texto.length(); i++) {
            if (texto.charAt(i) != separar) {
                linea = linea + String.valueOf(texto.charAt(i));
            } else {
                cadenas[contador] = linea;
                contador++;
                linea = "";
            }
        }
        return cadenas;
    }
}
