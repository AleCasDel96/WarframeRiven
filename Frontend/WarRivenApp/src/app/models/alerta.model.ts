export interface Alerta {
  fuente: string;
  mensaje: string;
  fechaExpiracion?: Date | null;
}