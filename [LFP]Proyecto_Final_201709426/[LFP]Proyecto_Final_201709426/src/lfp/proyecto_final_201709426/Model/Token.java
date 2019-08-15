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
public class Token {
    private String lexema;
    private int numero_token;
    private Object valor;
    private int fila;
    private int columna;
    private String tipo;

    public Token() {
    }
    
    public Token(String lexema, int numero_token, Object valor, int fila, int columna, String tipo) {
        this.lexema = lexema;
        this.numero_token = numero_token;
        this.valor = valor;
        this.fila = fila;
        this.columna = columna;
        this.tipo = tipo;
    }

    public String getLexema() {
        return lexema;
    }

    public void setLexema(String lexema) {
        this.lexema = lexema;
    }

    public int getNumero_token() {
        return numero_token;
    }

    public void setNumero_token(int numero_token) {
        this.numero_token = numero_token;
    }

    public Object getValor() {
        return valor;
    }

    public void setValor(Object valor) {
        this.valor = valor;
    }

    public int getFila() {
        return fila;
    }

    public void setFila(int fila) {
        this.fila = fila;
    }

    public int getColumna() {
        return columna;
    }

    public void setColumna(int columna) {
        this.columna = columna;
    }

    public String getTipo() {
        return tipo;
    }

    public void setTipo(String tipo) {
        this.tipo = tipo;
    }

    
    @Override
    public String toString() {
        return "lexema= " + lexema + "\t numero_token= " + numero_token + "\t valor= " + valor + "\t fila= " + fila + "\t columna= " + columna ;
    }
    
    
}
