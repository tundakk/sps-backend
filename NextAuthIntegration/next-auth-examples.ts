// // NextAuth.js example configuration for TypeScript frontend
// import NextAuth, { NextAuthOptions } from 'next-auth';
// import CredentialsProvider from 'next-auth/providers/credentials';

// // Types for JWT response
// interface AuthResponse {
//   status: string;
//   data: {
//     token: string;
//     expiresIn: number;
//     userId: string;
//     email: string;
//     userName: string;
//     roles: string[];
//   };
// }

// // Types for JWT token content
// interface JwtToken {
//   token: string;
//   expiresIn: number;
//   userId: string;
//   email: string;
//   userName: string;
//   roles: string[];
//   exp: number;
// }

// // Base API URL - replace with your own
// const API_URL = process.env.NEXT_PUBLIC_API_URL || 'https://localhost:7074';

// export const authOptions: NextAuthOptions = {
//   providers: [
//     CredentialsProvider({
//       name: 'Credentials',
//       credentials: {
//         email: { label: 'Email', type: 'email', placeholder: 'email@example.com' },
//         password: { label: 'Password', type: 'password' },
//       },
//       async authorize(credentials) {
//         if (!credentials?.email || !credentials?.password) {
//           return null;
//         }

//         try {
//           // Call your custom login endpoint
//           const response = await fetch(`${API_URL}/api/Auth/login`, {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify({
//               email: credentials.email,
//               password: credentials.password,
//             }),
//           });

//           if (!response.ok) {
//             console.error('Login failed:', response.status, response.statusText);
//             return null;
//           }

//           const authResponse: AuthResponse = await response.json();
//           if (authResponse.status !== 'success' || !authResponse.data) {
//             console.error('Auth response error:', authResponse);
//             return null;
//           }

//           // Return the JWT data which will be stored in the token
//           return {
//             id: authResponse.data.userId,
//             email: authResponse.data.email,
//             name: authResponse.data.userName,
//             roles: authResponse.data.roles,
//             token: authResponse.data.token,
//             expiresIn: authResponse.data.expiresIn,
//           };
//         } catch (error) {
//           console.error('Auth error:', error);
//           return null;
//         }
//       },
//     }),
//   ],
//   pages: {
//     signIn: '/auth/signin', // Custom sign-in page
//     error: '/auth/error', // Custom error page
//   },
//   callbacks: {
//     // JWT callback to include custom data in token
//     async jwt({ token, user }) {
//       if (user) {
//         // Add custom fields to the token
//         token.roles = user.roles;
//         token.token = user.token;
//         token.userId = user.id;
        
//         // Add expiry as a proper timestamp
//         const expiryTime = Math.floor(Date.now() / 1000) + user.expiresIn;
//         token.exp = expiryTime;
//       }
//       return token;
//     },
//     // Session callback to include custom data in session
//     async session({ session, token }) {
//       if (token) {
//         session.user.id = token.userId as string;
//         session.user.roles = token.roles as string[];
//         session.accessToken = token.token as string;
//         session.expires = new Date((token.exp as number) * 1000).toISOString();
//       }
//       return session;
//     },
//   },
//   session: {
//     strategy: 'jwt', // Use JWT strategy
//     maxAge: 12 * 60 * 60, // 12 hours (adjust to match your backend token expiration)
//   },
//   secret: process.env.NEXTAUTH_SECRET, // Secret used to sign JWTs
// };

// export default NextAuth(authOptions);