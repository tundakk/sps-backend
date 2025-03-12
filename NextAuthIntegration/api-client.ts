// // Example of authenticated API requests with TypeScript and NextAuth

// import { getSession } from 'next-auth/react';

// // Base API URL - should match your backend
// const API_URL = process.env.NEXT_PUBLIC_API_URL || 'https://localhost:7074';

// /**
//  * Fetch wrapper that automatically includes authentication and handles errors
//  */
// export async function fetchWithAuth(
//   endpoint: string,
//   options: RequestInit = {}
// ) {
//   // Get the current session
//   const session = await getSession();
  
//   // Default headers
//   const headers = {
//     'Content-Type': 'application/json',
//     ...options.headers,
//   };
  
//   // Add authorization header if we have a token
//   if (session?.accessToken) {
//     headers['Authorization'] = `Bearer ${session.accessToken}`;
//   }
  
//   // Make the request
//   const response = await fetch(`${API_URL}${endpoint}`, {
//     ...options,
//     headers,
//   });

//   // Handle error responses
//   if (!response.ok) {
//     // Handle 401 Unauthorized by redirecting to login
//     if (response.status === 401) {
//       // If using client-side navigation:
//       // router.push('/auth/signin');
      
//       // If using server-side redirection:
//       window.location.href = '/auth/signin';
      
//       throw new Error('Authentication required');
//     }
    
//     // General error handling
//     const errorData = await response.json().catch(() => ({}));
//     throw new Error(
//       errorData.message || 
//       `API error: ${response.status} ${response.statusText}`
//     );
//   }
  
//   // Return the successful response
//   return response.json();
// }

// // Example API client using our fetchWithAuth helper
// export const apiClient = {
//   // User-related endpoints
//   user: {
//     getCurrent: () => fetchWithAuth('/api/Auth/me'),
//   },
  
//   // Students endpoints (example)
//   students: {
//     getAll: () => fetchWithAuth('/api/Student'),
//     getById: (id: string) => fetchWithAuth(`/api/Student/${id}`),
//     create: (data: any) => fetchWithAuth('/api/Student', {
//       method: 'POST',
//       body: JSON.stringify(data),
//     }),
//     update: (id: string, data: any) => fetchWithAuth(`/api/Student/${id}`, {
//       method: 'PUT',
//       body: JSON.stringify(data),
//     }),
//     delete: (id: string) => fetchWithAuth(`/api/Student/${id}`, {
//       method: 'DELETE',
//     }),
//   },
  
//   // Add other API endpoints as needed
// };

// // Example of using the API client in a component:
// /*
// import { useEffect, useState } from 'react';
// import { apiClient } from './api-client';

// export function StudentList() {
//   const [students, setStudents] = useState([]);
//   const [loading, setLoading] = useState(true);
//   const [error, setError] = useState(null);
  
//   useEffect(() => {
//     async function fetchStudents() {
//       try {
//         setLoading(true);
//         const data = await apiClient.students.getAll();
//         setStudents(data.data);
//       } catch (err) {
//         setError(err.message);
//       } finally {
//         setLoading(false);
//       }
//     }
    
//     fetchStudents();
//   }, []);
  
//   if (loading) return <div>Loading...</div>;
//   if (error) return <div>Error: {error}</div>;
  
//   return (
//     <div>
//       <h1>Students</h1>
//       <ul>
//         {students.map(student => (
//           <li key={student.id}>{student.name}</li>
//         ))}
//       </ul>
//     </div>
//   );
// }
// */