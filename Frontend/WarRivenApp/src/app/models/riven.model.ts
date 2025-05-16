export interface Riven {
  id: string;
  nombre: string;
  arma: string;
  idUsuario?: string;

  stat1: string;
  valor1: number;

  stat2?: string;
  valor2?: number;

  stat3?: string;
  valor3?: number;

  stat4?: string;
  valor4?: number;

  tieneOferta?: boolean;
}
