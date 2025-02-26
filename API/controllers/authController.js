// importing config file
import config from '../config.js';
// importing jsonwebtoken library for handling JWTs
import jwt from 'jsonwebtoken';
// importing User model
import User from '../models/user.js';

// exporting register method
export const register = async (rq, rs) => {
    // Setting admin field to false in the request body
    rq.body.admin = false;
    
    // creating a new user and saving it to the database
    await new User(rq.body).save()
        .then((user) => {
            console.log(user);

            // creating a JWT token for the user with their userId and a secret key
            const token = jwt.sign({ userId: user._id }, config.SECRET_KEY, {
                expiresIn: '1 hour'
            });

            // sending the token back to the client with a 201 status code
            rs.status(201).json({ token });
        })
        .catch((error) => {
            console.log(error);

            // handling duplicate key error (e.g., username or email already in use)
            if (error.code == 11000) 
                return rs.status(500).json({ code: 500, msg: 'Username or email is already used' });

            // sending a generic error response if user creation fails
            rs.status(500).json({ code: 500, msg: 'Unable to create user' });
        });
};

// exporting login method
export const login = async (rq, rs) => {
    // creating a search object to find the user by email or username
    let searcher = { email: rq.body.email };
    if (!rq.body.email) searcher = { username: rq.body.username };

    // finding a user that matches the search criteria
    await User.findOne(searcher)
        .then((user) => {
            // if user is not found, throw an error
            if (!user) throw new Error('err');

            // comparing the provided password with the stored hashed password
            const passwordMatch = user.comparePassword(rq.body.password);
            // if passwords do not match, send a 401 response
            if (!passwordMatch) return rs.status(401).json({ code: 401, msg: 'Wrong password' });

            // creating a JWT token for the user with their userId and a secret key
            const token = jwt.sign({ userId: user._id }, config.SECRET_KEY, {
                expiresIn: '1 hour'
            });

            // sending the token back to the client with a 201 status code
            rs.status(201).json({ token });
        })
        .catch((error) => {
            console.log(error);  // Logging the error to the console

            // sending a generic error response if user is not found or any other error occurs
            rs.status(500).json({ code: 500, msg: 'Unable to find user' });
        });
};


