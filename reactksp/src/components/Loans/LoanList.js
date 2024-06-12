import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import loanService from '../../services/loanService';
import { useAuth } from '../../context/AuthProvider';

const LoanList = () => {
    const [loans, setLoans] = useState([]);
    const { auth } = useAuth();

    useEffect(() => {
        const fetchLoans = async () => {
            const loans = await loanService.getLoans(auth);
            setLoans(loans);
        };

        fetchLoans();
    }, [auth]);

    const handleReturn = async (id) => {
        await loanService.returnBook(id, auth);
        setLoans(loans.filter(loan => loan.id !== id));
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Loans</h2>
            <Link to="/add-loan">Add Loan</Link>
            <ul>
                {loans.map((loan) => (
                    <li key={loan.id}>
                        User: {loan.userId}, Book: {loan.bookId}, Date: {loan.loanDate}
                        <button onClick={() => loanService.returnBook(loan.id, auth)}>Return Book</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default LoanList;
