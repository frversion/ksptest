import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import userService from '../../services/userService';
import { useAuth } from '../../context/AuthProvider';

const UserList = (auth) => {
    const [users, setUsers] = useState([]);
    //const { auth } = useAuth();

    useEffect(() => {
        const fetchUsers = async (auth) => {
            const users = await userService.getUsers(auth);
            setUsers(users);
        };

        fetchUsers();
    }, [auth]);
    //}, []);

    const handleDelete = async (id) => {
        await userService.deleteUser(id); //, auth);
        setUsers(users.filter(user => user.id !== id));
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Users</h2>
            <Link to="/AddUser">Add User</Link>
            <ul>
                {users.map((user) => (
                    <li key={user.id}>
                        {user.id}, {user.username} - ({user.email})
                        <Link to={`/EditUser1/${user.id}`}>Edit</Link>
                        <button onClick={() => handleDelete(user.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default UserList;
