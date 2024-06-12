import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import Login from './components/Login';
import BookList from './components/Books/BookList';
import AddBook from './components/Books/AddBook';
import EditBook from './components/Books/EditBook';
import UserList from './components/Users/UserList';
import AddUser from './components/Users/AddUser';
import EditUser from './components/Users/EditUser';
import LoanList from './components/Loans/LoanList';
import AddLoan from './components/Loans/AddLoan';
import { AuthProvider, useAuth } from './context/AuthProvider';

const ProtectedRoute = ({ children }) => {
    const { auth } = useAuth();
    return auth ? children : <Navigate to="/login" />;
};

const App = () => {
    return (
        <AuthProvider>
                <Routes>
                    <Route path="/login" element={<Login />} />
                    <Route path="/books" element={<ProtectedRoute><BookList /></ProtectedRoute>} />
                    <Route path="/add-book" element={<ProtectedRoute><AddBook /></ProtectedRoute>} />
                    <Route path="/edit-book/:id" element={<ProtectedRoute><EditBook /></ProtectedRoute>} />
                    <Route path="/users" element={<ProtectedRoute><UserList /></ProtectedRoute>} />
                    <Route path="/add-user" element={<ProtectedRoute><AddUser /></ProtectedRoute>} />
                    <Route path="/edit-user/:id" element={<ProtectedRoute><EditUser /></ProtectedRoute>} />
                    <Route path="/loans" element={<ProtectedRoute><LoanList /></ProtectedRoute>} />
                    <Route path="/add-loan" element={<ProtectedRoute><AddLoan /></ProtectedRoute>} />
                </Routes>
        </AuthProvider>
    );
};

export default App;
