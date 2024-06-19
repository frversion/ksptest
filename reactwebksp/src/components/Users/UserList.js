import React, { useEffect, useState } from 'react';
import userService from '../../services/userService';
import { useNavigate } from 'react-router-dom';
//import { useAuth } from '../../context/AuthProvider';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';

const UserList = () => {

    const navigate = useNavigate();
    const auth = localStorage.getItem("token");

    if (!auth) {
        navigate("/login");
    } 

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const [users, setUsers] = useState([]);

    useEffect(() => {
        const fetchUsers = async (auth) => {
            var token = "";
            if (!auth) {
                token = localStorage.getItem("token");
            }
            else {
                token = auth;
            }
            const users = await userService.getUsers(token);
            setUsers(users);
        };

        fetchUsers();
    }, [auth]);

    const handleDelete = async (id, auth) => {
        var token = "";
        if (!auth) {
            token = localStorage.getItem("token");
        }
        else {
            token = auth;
        }

        setError('');
        setSuccess('');

        try {
            const response = await userService.deleteUser(id, token);

            if (response.isSuccess) {
                setSuccess(response.resultMessage);
                setTimeout(() => {
                    navigate('/UserList'); // Redirect to the book list
                }, 2000);
            }
            else {
                setError(response.errorMessage || "Ocurrio un error.");
            }
        }
        catch (err) {
            setError(err.Message)
        }
    };

    return (
        <div>
            {error && <Alert variant="danger">{error}</Alert>}
            {success && <Alert variant="success">{success}</Alert>}
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th align="center">#</th>
                        <th>Nombre del usuario</th>
                        <th>Email</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {users.map((user) => (
                        <tr>
                            <td align="center" key={user.id}>{user.id}</td>
                            <td>{user.username}</td>
                            <td>{user.email}</td>
                            <td align="center"><Button variant="outline-danger" size="sm" onClick={() => handleDelete(user.id, auth)}>Eliminar</Button></td>
                        </tr>
                    ))}
                </tbody>
            </Table>
            <Button variant="primary" href={"/AddUser/"}>Agregar usuario</Button>
        </div>
    );
};

export default UserList;
