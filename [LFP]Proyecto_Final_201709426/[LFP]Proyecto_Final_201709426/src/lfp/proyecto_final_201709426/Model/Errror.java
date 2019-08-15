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
public class Errror extends Token{
    private String descripcion;
    public Errror() {
    }

    public Errror(String descripcion, String lexema, int numero_token, Object valor, int fila, int columna, String tipo) {
        super(lexema, numero_token, valor, fila, columna, tipo);
        this.descripcion = descripcion;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    @Override
    public String toString() {
        return super.toString() + "Descripcion: " + getDescripcion();
    }
    
}
