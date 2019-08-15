/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package lfp.proyecto_final_201709426.Controller;

import java.util.ArrayList;
import javax.swing.JOptionPane;
import lfp.proyecto_final_201709426.Model.Errror;
import lfp.proyecto_final_201709426.Model.Noticia;
import lfp.proyecto_final_201709426.Model.Token;

/**
 *
 * @author Samuel
 */
public class Analisis_Sintactico {

    ArrayList<Token> lista_token = new ArrayList();
    ArrayList<Errror> errores_sintacticos = new ArrayList();
    Noticia noticia = new Noticia();
    String titulo = "";
    String autor = "";
    String etiqueta_p = "";
    String contenido = "";
    String ruta_img = "";
    String[] rutas = new String[3];
    int numero_img = 0;
    int cambio = 0;

    int index = 0;

    public Analisis_Sintactico(ArrayList<Token> lista_token, ArrayList<Errror> errores_sintaciticos, Noticia noticia) {
        this.lista_token = lista_token;
        this.errores_sintacticos = errores_sintaciticos;
        this.noticia = noticia;
    }

    public void etiquetas_principales() {
        try {
            while (lista_token.get(index).getNumero_token() != 999) {
                if (lista_token.get(index).getLexema().equals("<")) {
                    consumir();
                    if (lista_token.get(index).getLexema().equals("Noticia")) {
                        etiqueta_p = "Noticia";
                        consumir();
                    } else if (lista_token.get(index).getLexema().equals("Farandula")) {
                        etiqueta_p = "Farandula";
                        consumir();
                    } else if (lista_token.get(index).getLexema().equals("Deportes")) {
                        etiqueta_p = "Deportes";
                        consumir();
                    } else if (lista_token.get(index).getLexema().equals("Publicidad")) {
                        etiqueta_p = "Publicidad";
                        consumir();
                    } else {
                        capturar_error("una etiqueta principal");
                        etiqueta_p = "Noticia";
                    }
                    if (lista_token.get(index).getLexema().equals(">")) {
                        consumir();
                    } else {
                        capturar_error(">");
                    }
                    try {
                        while (!(lista_token.get(index + 1).getLexema().equals("/"))
                                && !(lista_token.get(index + 2).getLexema().equals(etiqueta_p))) {

                            etiquetas_secundarias();
                        }
                        if (lista_token.get(index).getLexema().equals("<")) {
                            consumir();
                        } else {
                            capturar_error("<");
                        }
                        if (lista_token.get(index).getLexema().equals("/")) {
                            consumir();
                        } else {
                            capturar_error("/");
                        }

                        if (lista_token.get(index).getLexema().equals(etiqueta_p)) {
                            consumir();
                        } else {
                            capturar_error("etiqueta " + etiqueta_p);
                        }
                        if (lista_token.get(index).getLexema().equals(">")) {
                            consumir();
                        } else {
                            capturar_error(">");
                        }
                    } catch (Exception e) {
                        capturar_error("cerrar la etiqueta " + etiqueta_p);
                    }

                } else {
                    capturar_error("Abrir una etiqueta");
                }

            }
        } catch (Exception e) {
        }

        noticia.setAutor(autor);
        noticia.setTitulo(titulo);
        noticia.setImagen(rutas);
        noticia.setContenido(contenido);
        noticia.setTipo(etiqueta_p);
    }

    public void etiquetas_secundarias() {
        try {
            if (lista_token.get(index).getLexema().equals("<")) {
                consumir();
                if (lista_token.get(index).getLexema().equals("Titulo")) {
                    etiqueta_titulo();
                } else if (lista_token.get(index).getLexema().equals("Autor")) {
                    etiqueta_autor();
                } else if (lista_token.get(index).getLexema().equals("Contenido")) {
                    etiqueta_contenido();
                } else {
                    capturar_error("una etiqueta secundaria (Titulo, Autor o Contenido)");
                }
            } else {
                capturar_error("Abrir una etiqueta");
            }

        } catch (Exception e) {
        }

    }

    public void etiqueta_contenido() {

        if (lista_token.get(index).getLexema().equals("Contenido")) {
            consumir();
        }
        if ((lista_token.get(index).getLexema()).equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
        int fila = 0;
        try {
            while (!lista_token.get(index + 1).getLexema().equals("/")
                    && !lista_token.get(index + 2).getLexema().equals("Contenido")) {

                if (lista_token.get(index).getLexema().equals("<")) {
                    if (lista_token.get(index + 1).getLexema().equals("ImgUno")
                            || lista_token.get(index + 1).getLexema().equals("ImgDos")
                            || lista_token.get(index + 1).getLexema().equals("ImgTres")) {
                        etiqueta_img();
                    } else {
                        capturar_error("una etiqueta img");
                    }

                } else if (lista_token.get(index).getLexema().equals("'")) {
                    consumir();
                    if (lista_token.get(index).getNumero_token() == 2) {
                        consumir();
                    } else {
                        capturar_error("un numero");
                    }
                    if (lista_token.get(index).getLexema().equals("/")) {
                        consumir();
                    } else {
                        capturar_error("/");
                    }
                    if (lista_token.get(index).getNumero_token() == 2) {
                        consumir();
                    } else {
                        capturar_error("un numero");
                    }
                    if (lista_token.get(index).getLexema().equals("/")) {
                        consumir();
                    } else {
                        capturar_error("/");
                    }
                    if (lista_token.get(index).getNumero_token() == 2) {
                        consumir();
                    } else {
                        capturar_error("un numero");
                    }
                    if (lista_token.get(index).getLexema().equals("'")) {
                        consumir();
                    } else {
                        capturar_error("'");
                    }
                } else {
                    if (lista_token.get(index).getFila() != fila) {
                        contenido = contenido + "\n";
                    }
                    contenido = contenido + " " + lista_token.get(index).getLexema();
                    consumir();
                    fila = lista_token.get(index + 1).getFila();
                }
            }
        } catch (Exception e) {
        }

        if (lista_token.get(index).getLexema().equals("<")) {
            consumir();
        } else {
            capturar_error("<");
        }
        if (lista_token.get(index).getLexema().equals("/")) {
            consumir();
        } else {
            capturar_error("/");
        }
        if (lista_token.get(index).getLexema().equals("Contenido")) {
            consumir();
        } else {
            capturar_error("Contenido");
        }
        if (lista_token.get(index).getLexema().equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
    }

    public void etiqueta_img() {
        String etiqueta = "";
        if (lista_token.get(index).getLexema().equals("<")) {
            consumir();
        }
        if (lista_token.get(index).getLexema().equals("ImgUno")) {
            etiqueta = "ImgUno";
            numero_img = 0;
            consumir();
        } else if (lista_token.get(index).getLexema().equals("ImgDos")) {
            etiqueta = "ImgDos";
            numero_img = 1;
            consumir();
        } else if (lista_token.get(index).getLexema().equals("ImgTres")) {
            etiqueta = "ImgTres";
            numero_img = 2;
            consumir();
        } else {
            capturar_error("una etiqueta img");
        }
        if (lista_token.get(index).getLexema().equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
        if (lista_token.get(index).getLexema().equals("\"")) {
            consumir();
        } else {
            capturar_error("\"");
        }
        try {
            while (!lista_token.get(index).getLexema().equals("\"")) {
                ruta_img = ruta_img + lista_token.get(index).getLexema();
                consumir();
            }
        } catch (Exception e) {
        }

        if (lista_token.get(index).getLexema().equals("\"")) {
            consumir();
        } else {
            capturar_error("\"");
        }
        if (lista_token.get(index).getLexema().equals("<")) {
            consumir();
        } else {
            capturar_error("<");
        }
        if (lista_token.get(index).getLexema().equals("/")) {
            consumir();
        } else {
            capturar_error("/");
        }

        if (lista_token.get(index).getLexema().equals(etiqueta)) {
            consumir();
        } else {
            capturar_error(etiqueta);
        }
        if (lista_token.get(index).getLexema().equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
        rutas[numero_img] = ruta_img;
        ruta_img = "";
        numero_img = 0;

    }

    public void etiqueta_autor() {
        if (lista_token.get(index).getLexema().equals("Autor")) {
            consumir();
        }
        if ((lista_token.get(index).getLexema()).equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
        while (!lista_token.get(index + 1).getLexema().equals("/")
                && !lista_token.get(index + 2).getLexema().equals("Autor")) {
            autor = autor + " " + lista_token.get(index).getLexema();
            consumir();
        }
        if (lista_token.get(index).getLexema().equals("<")) {
            consumir();
        } else {
            capturar_error("<");
        }
        if (lista_token.get(index).getLexema().equals("/")) {
            consumir();
        } else {
            capturar_error("/");
        }
        if (lista_token.get(index).getLexema().equals("Autor")) {
            consumir();
        } else {
            capturar_error("Autor");
        }
        if (lista_token.get(index).getLexema().equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
    }

    public void etiqueta_titulo() {

        if (lista_token.get(index).getLexema().equals("Titulo")) {
            consumir();
        }
        if ((lista_token.get(index).getLexema()).equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
        while (!lista_token.get(index + 1).getLexema().equals("/")
                && !lista_token.get(index + 2).getLexema().equals("Titulo")) {
            titulo = titulo + " " + lista_token.get(index).getLexema();
            consumir();
            if (lista_token.get(index).getNumero_token() == 999) {
                break;
            }
        }
        if (lista_token.get(index).getLexema().equals("<")) {
            consumir();
        } else {
            capturar_error("<");
        }
        if (lista_token.get(index).getLexema().equals("/")) {
            consumir();
        } else {
            capturar_error("/");
        }
        if (lista_token.get(index).getLexema().equals("Titulo")) {
            consumir();
        } else {
            capturar_error("Titulo");
        }
        if (lista_token.get(index).getLexema().equals(">")) {
            consumir();
        } else {
            capturar_error(">");
        }
    }

    public void consumir() {
        index++;
        cambio = 1;
    }

    public void capturar_error(String esperado) {
        esperado = "Error sintactico, se esperaba " + esperado;
        errores_sintacticos.add(new Errror(esperado, lista_token.get(index).getLexema(),
                lista_token.get(index).getNumero_token(), lista_token.get(index).getValor(),
                lista_token.get(index).getFila(), lista_token.get(index).getColumna(), lista_token.get(index).getTipo()));

        JOptionPane.showMessageDialog(null, esperado);
        consumir();
    }

    public void etiqueta_inicial() {
        int aux = index;

    }
}
