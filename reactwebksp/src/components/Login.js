import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import authService from '../services/authService';
import { useAuth } from '../context/AuthProvider';
import { Alert, Form, Button } from 'react-bootstrap';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { setAuth } = useAuth();

    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        setError('');

        if (!email || !password) {
            setError('Debe ingresar email y password.');
            return;
        }

        try {
            const token = await authService.login(email, password );
            setAuth(token);
            navigate('/BookList');
            localStorage.setItem('token', token)
        } catch (error) {
            setError('No fue posible autenticar al usuario.', error);
        }
    };

    return (
        <div className="container">
            <h2>Login</h2>
            {error && <Alert variant="danger">{error}</Alert>}
            <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formUsername">
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                </Form.Group>
                <Form.Group controlId="formPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </Form.Group>
                <br/>
                &nbsp;
                <Button variant="primary" type="submit">Login</Button>
            </Form>
        </div>
    //    <div style={{ padding: '20px' }}>
    //        <h2>Login</h2>
    //        <form onSubmit={handleSubmit}>
    //            <div>
    //                <label>Email:</label>
    //                <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
    //            </div>
    //            <div>
    //                <label>Password:</label>
    //                <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
    //            </div>
    //            <button type="submit">Login</button>
    //        </form>
    //    </div>
    );
};

export default Login;
