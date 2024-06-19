import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import userService from '../../services/userService';
//import { useAuth } from '../../context/AuthProvider';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { Alert } from 'react-bootstrap';

const AddUser = () => {

    const navigate = useNavigate();
    const auth = localStorage.getItem("token");

    if (!auth) {
        navigate("/login");
    } 

    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    //const { auth } = useAuth();
    const history = useNavigate();

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        setError('');
        setSuccess('');

        if (!username || !email || !password) {
            setError('Todos los campos son obligatorios.');
            return;
        }

        try {
            const response = await userService.addUser({ username, email, password }, auth);

            if (response.isSuccess) {
                setSuccess('User agregado con exito.');
                setTimeout(() => {
                    navigate('/UserList'); // Redirect to the users list
                }, 2000);
            } else {
                setError(response.errorMessage || 'Un error ha ocurrido.');
            }
        } catch (err) {
            setError(err.Message);
        }
        
    };

    const handleCancel = () => {
        navigate('/UserList');
    };

    return (
        <div>
            {error && <Alert variant="danger">{error}</Alert>}
            {success && <Alert variant="success">{success}</Alert>}
        <form onSubmit={handleSubmit}>
            <Form>
                <Form.Group className="mb-3" controlId="FormAddUser.ControlInput1">
                    <Form.Label>Nombre de usuario</Form.Label>
                    <Form.Control type="text" value={username} onChange={(e) => setUsername(e.target.value)} required autoFocus />
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="email" value={email} placeholder="name@ksp.com" onChange={(e) => setEmail(e.target.value)} required />
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </Form.Group>
            </Form>
            <Button variant="primary" type="button" onClick={handleCancel}>Cancelar</Button>
                &nbsp;
            <Button variant="primary" type="submit">Guardar</Button>
            
        </form>
        </div>
    );
};

export default AddUser;
