export interface User {
  id: number;
  name: string;
  surname: string;
  phone?: string;
  email: string;
  password?: string;
  [key:string]: any;
}