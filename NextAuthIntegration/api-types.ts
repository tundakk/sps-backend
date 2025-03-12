// // TypeScript interfaces for the API response types

// // Base service response interface
// export interface ServiceResponse<T> {
//   status: 'success' | 'error';
//   data?: T;
//   message?: string;
//   errorCode?: string;
//   technicalDetails?: string;
//   validationErrors?: string[];
// }

// // Auth related interfaces
// export interface LoginRequest {
//   email: string;
//   password: string;
// }

// export interface RegisterRequest {
//   email: string;
//   password: string;
//   confirmPassword: string;
// }

// export interface AuthResponseData {
//   token: string;
//   expiresIn: number;
//   userId: string;
//   email: string;
//   userName: string;
//   roles: string[];
// }

// export interface UserData {
//   id: string;
//   email: string;
//   userName: string;
//   roles: string[];
// }

// // Example of extending NextAuth session with our custom properties
// declare module "next-auth" {
//   interface Session {
//     accessToken?: string;
//     user: {
//       id: string;
//       name?: string | null;
//       email?: string | null;
//       image?: string | null;
//       roles: string[];
//     };
//   }

//   interface User {
//     id: string;
//     name?: string | null;
//     email?: string | null;
//     image?: string | null;
//     roles: string[];
//     token: string;
//     expiresIn: number;
//   }
// }

// // Example business model interfaces (based on your domain)
// export interface Student {
//   id: string;
//   firstName: string;
//   lastName: string;
//   email: string;
//   phoneNumber: string;
//   // Add other properties as needed
// }

// export interface Education {
//   id: string;
//   name: string;
//   description: string;
//   // Add other properties as needed
// }

// // Add more interfaces for your domain models as needed