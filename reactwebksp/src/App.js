import logo from './logo.svg';
import './App.css';
import React from 'react';
import { Routes, Route, Link, Navigate } from 'react-router-dom';
import Login from './components/Login';
import BookList from './components/Books/BookList';
import AddBook from './components/Books/AddBook';
import EditBook from './components/Books/EditBook';
import UserList from './components/Users/UserList';
import AddUser from './components/Users/AddUser';
import EditUser1 from './components/Users/EditUser1';
import LoanList from './components/Loans/LoanList';
import AddLoan from './components/Loans/AddLoan';

import { AuthProvider, useAuth } from './context/AuthProvider';

const ProtectedRoute = ({ children }) => {
    const { auth } = useAuth();
    return auth ? children : <Navigate to="/login" />;
};

//const App = () => {
//    return (
//        /*<AuthProvider>*/
//            <Routes>
//                <Route path="/BookList" element={<BookList />} />
//            </Routes>
//        /*</AuthProvider>*/
//    );
//};

const App = () => {
    return (
        <div className="container">
            <nav>
                <ul>
                    <li>
                        <Link to="/Login">Login</Link>
                    </li>
                    <li>
                        <Link to="/BookList">Libros</Link>
                    </li>
                    <li>
                        <Link to="/UserList">Usuarios</Link>
                    </li>
                    <li>
                        <Link to="/LoanList">Prestamos</Link>
                    </li>
                </ul>
            </nav>
            <AuthProvider>
                <Routes>
                    <Route path="/login" element={<Login />} />
                    <Route path="/BookList" element={<BookList />} />
                    <Route path="/AddBook" element={<AddBook />} />
                    <Route path="/EditBook/:id" element={<EditBook />} />

                    <Route path="/UserList" element={<UserList />} />
                    <Route path="/AddUser" element={<AddUser />} />
                    <Route path="/EditUser1" element={<EditUser1 />} />

                    <Route path="/LoanList" element={<LoanList />} />
                    <Route path="/AddLoan" element={<AddLoan /> } />
                </Routes>
            </AuthProvider>
        </div>
    );
};

//App.use(function (req, res, next) {
//    res.header('Access-Control-Allow-Origin', 'http://localhost:3242');
//    res.header(
//        'Access-Control-Allow-Headers',
//        'Origin, X-Requested-With, Content-Type, Accept'
//    );
//    next();
//});

//App.use(cors());

export default App;
