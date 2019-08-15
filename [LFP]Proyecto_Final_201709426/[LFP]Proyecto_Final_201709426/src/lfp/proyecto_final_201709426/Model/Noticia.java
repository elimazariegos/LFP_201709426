/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package lfp.proyecto_final_201709426.Model;

/**
 *
 * @author Samuel
 */
public class Noticia {

    private String titulo;
    private String autor;
    private String contenido;
    private String[] imagen;
    private String tipo;

    public Noticia() {
    }

    public Noticia(String titulo, String autor, String contenido, String[] imagen, String tipo) {
        this.titulo = titulo;
        this.autor = autor;
        this.contenido = contenido;
        this.imagen = imagen;
        this.tipo = tipo;
    }

    public String getTitulo() {
        return titulo;
    }

    public void setTitulo(String titulo) {
        this.titulo = titulo;
    }

    public String getAutor() {
        return autor;
    }

    public void setAutor(String autor) {
        this.autor = autor;
    }

    public String getContenido() {
        return contenido;
    }

    public void setContenido(String contenido) {
        this.contenido = contenido;
    }

    public String[] getImagen() {
        return imagen;
    }

    public void setImagen(String[] imagen) {
        this.imagen = imagen;
    }

    public String getTipo() {
        return tipo;
    }

    public void setTipo(String tipo) {
        this.tipo = tipo;
    }
    
    
    
}
