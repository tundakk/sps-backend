# NextAuth.js Integration Guide

This guide provides complete integration examples for using the SPS Backend API with NextAuth.js in a Next.js frontend application.

## 📋 Overview

The SPS Backend is designed to work seamlessly with NextAuth.js, providing:
- JWT token authentication
- Role-based access control
- Secure session management
- TypeScript type definitions

## 🚀 Quick Setup

### 1. Install Dependencies

```bash
npm install next-auth
npm install axios  # or your preferred HTTP client
```

### 2. Configuration Files

Copy the integration files from the `NextAuthIntegration` folder:
- `api-types.ts` - TypeScript definitions
- `api-client.ts` - HTTP client with authentication
- `next-auth-examples.ts` - NextAuth configuration examples

### 3. Environment Variables

Create a `.env.local` file:

```env
NEXTAUTH_URL=http://localhost:3000
NEXTAUTH_SECRET=your-secret-key-here
NEXT_PUBLIC_API_URL=https://localhost:5001
```

## 🔧 Implementation

### NextAuth Configuration

Create `pages/api/auth/[...nextauth].ts`:

```typescript
import NextAuth, { NextAuthOptions } from 'next-auth'
import CredentialsProvider from 'next-auth/providers/credentials'

export const authOptions: NextAuthOptions = {
  providers: [
    CredentialsProvider({
      name: 'credentials',
      credentials: {
        email: { label: 'Email', type: 'email' },
        password: { label: 'Password', type: 'password' }
      },
      async authorize(credentials) {
        if (!credentials?.email || !credentials?.password) {
          return null
        }

        try {
          const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/auth/login`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              email: credentials.email,
              password: credentials.password,
            }),
          })

          if (!response.ok) {
            return null
          }

          const result = await response.json()
          
          if (result.status === 'success' && result.data.token) {
            return {
              id: result.data.userId,
              email: result.data.email,
              name: result.data.userName,
              token: result.data.token,
              roles: result.data.roles,
              expiresIn: result.data.expiresIn
            }
          }
          
          return null
        } catch (error) {
          console.error('Authentication error:', error)
          return null
        }
      }
    })
  ],
  callbacks: {
    async jwt({ token, user }) {
      if (user) {
        token.accessToken = user.token
        token.roles = user.roles
        token.expiresIn = user.expiresIn
      }
      return token
    },
    async session({ session, token }) {
      session.accessToken = token.accessToken as string
      session.user.roles = token.roles as string[]
      return session
    }
  },
  pages: {
    signIn: '/auth/signin',
    error: '/auth/error',
  },
  session: {
    strategy: 'jwt',
  },
}

export default NextAuth(authOptions)
```

### API Client with Authentication

Create `lib/api-client.ts`:

```typescript
import axios, { AxiosInstance, AxiosRequestConfig } from 'axios'
import { getSession } from 'next-auth/react'

class ApiClient {
  private client: AxiosInstance

  constructor() {
    this.client = axios.create({
      baseURL: process.env.NEXT_PUBLIC_API_URL,
      headers: {
        'Content-Type': 'application/json',
      },
    })

    // Request interceptor to add auth token
    this.client.interceptors.request.use(
      async (config) => {
        const session = await getSession()
        if (session?.accessToken) {
          config.headers.Authorization = `Bearer ${session.accessToken}`
        }
        return config
      },
      (error) => {
        return Promise.reject(error)
      }
    )

    // Response interceptor for error handling
    this.client.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error.response?.status === 401) {
          // Handle unauthorized - redirect to login
          window.location.href = '/auth/signin'
        }
        return Promise.reject(error)
      }
    )
  }

  // Generic request method
  async request<T>(config: AxiosRequestConfig): Promise<T> {
    const response = await this.client.request<T>(config)
    return response.data
  }

  // Convenience methods
  async get<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    return this.request<T>({ ...config, method: 'GET', url })
  }

  async post<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
    return this.request<T>({ ...config, method: 'POST', url, data })
  }

  async put<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
    return this.request<T>({ ...config, method: 'PUT', url, data })
  }

  async delete<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    return this.request<T>({ ...config, method: 'DELETE', url })
  }
}

export const apiClient = new ApiClient()
```

### TypeScript Definitions

Create `types/api.ts`:

```typescript
// Authentication Types
export interface LoginRequest {
  email: string
  password: string
}

export interface AuthResponse {
  status: string
  data: {
    token: string
    expiresIn: number
    userId: string
    email: string
    userName: string
    roles: string[]
  }
}

// Student Types
export interface Student {
  id: string
  email: string
  firstName: string
  lastName: string
  phoneNumber?: string
  address?: string
  enrollmentDate: string
  status: StudentStatus
}

export enum StudentStatus {
  Active = 'Active',
  Inactive = 'Inactive',
  Graduated = 'Graduated'
}

// SPSA Case Types
export interface SPSACase {
  id: string
  studentId: string
  educationId: string
  status: SPSACaseStatus
  applicationDate: string
  decisionDate?: string
  approvedAmount?: number
  description: string
}

export enum SPSACaseStatus {
  Pending = 'Pending',
  UnderReview = 'UnderReview',
  Approved = 'Approved',
  Rejected = 'Rejected',
  Completed = 'Completed'
}

// API Response wrapper
export interface ApiResponse<T> {
  status: 'success' | 'error'
  data?: T
  message?: string
  errors?: string[]
}

// Pagination
export interface PaginatedResponse<T> {
  items: T[]
  totalCount: number
  pageNumber: number
  pageSize: number
  totalPages: number
}
```

### Using the API Client

Example component using the API:

```typescript
import { useEffect, useState } from 'react'
import { useSession } from 'next-auth/react'
import { apiClient } from '../lib/api-client'
import { Student, ApiResponse, PaginatedResponse } from '../types/api'

export default function StudentsPage() {
  const { data: session, status } = useSession()
  const [students, setStudents] = useState<Student[]>([])
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    if (status === 'authenticated') {
      fetchStudents()
    }
  }, [status])

  const fetchStudents = async () => {
    try {
      setLoading(true)
      const response = await apiClient.get<ApiResponse<PaginatedResponse<Student>>>('/api/students')
      
      if (response.status === 'success' && response.data) {
        setStudents(response.data.items)
      }
    } catch (error) {
      console.error('Failed to fetch students:', error)
    } finally {
      setLoading(false)
    }
  }

  if (status === 'loading') {
    return <div>Loading...</div>
  }

  if (status === 'unauthenticated') {
    return <div>Please sign in to access this page.</div>
  }

  return (
    <div>
      <h1>Students</h1>
      {loading ? (
        <div>Loading students...</div>
      ) : (
        <ul>
          {students.map((student) => (
            <li key={student.id}>
              {student.firstName} {student.lastName} - {student.email}
            </li>
          ))}
        </ul>
      )}
    </div>
  )
}
```

### Role-Based Access Control

Implement role-based components:

```typescript
import { useSession } from 'next-auth/react'

interface RoleGuardProps {
  allowedRoles: string[]
  children: React.ReactNode
  fallback?: React.ReactNode
}

export function RoleGuard({ allowedRoles, children, fallback }: RoleGuardProps) {
  const { data: session } = useSession()
  
  const userRoles = session?.user?.roles || []
  const hasAccess = allowedRoles.some(role => userRoles.includes(role))
  
  if (!hasAccess) {
    return <>{fallback || <div>Access denied</div>}</>
  }
  
  return <>{children}</>
}

// Usage
export default function AdminPanel() {
  return (
    <RoleGuard allowedRoles={['Admin', 'Staff']}>
      <div>Admin content here</div>
    </RoleGuard>
  )
}
```

## 🔒 Security Considerations

### JWT Token Handling
- Tokens are automatically included in requests
- Expired tokens trigger re-authentication
- Secure storage in NextAuth session

### CSRF Protection
- NextAuth provides built-in CSRF protection
- API uses proper CORS configuration
- Secure cookie settings in production

### Error Handling
```typescript
try {
  const response = await apiClient.get('/api/students')
  // Handle success
} catch (error) {
  if (error.response?.status === 401) {
    // Handle unauthorized
  } else if (error.response?.status === 403) {
    // Handle forbidden
  } else {
    // Handle other errors
  }
}
```

## 🚀 Best Practices

1. **Session Management**: Always check session status before API calls
2. **Error Boundaries**: Implement error boundaries for API failures
3. **Loading States**: Show loading indicators during API requests
4. **Type Safety**: Use TypeScript definitions for all API interactions
5. **Caching**: Implement appropriate caching for performance
6. **Rate Limiting**: Handle rate limit responses gracefully

## 📚 Complete Examples

See the `NextAuthIntegration` folder for complete working examples:
- Full authentication flow
- Protected routes
- API service implementations
- TypeScript type definitions
