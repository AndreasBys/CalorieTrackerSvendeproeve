import jwt from 'jsonwebtoken'; 

// importing config file
import config from '../config.js';
// importing user model
import User from '../models/user.js'; 

// exporting register method
export const register = async (req, res) => {
    // Setting admin field to false in the request body
    req.body.admin = false;

    // creating a new user and saving it to the database
    await new User(req.body).save()
        .then((user) => {
            console.log(user);

            // creating a JWT token for the user with their userId and a secret key
            const token = jwt.sign({ userId: user._id }, config.SECRET_KEY, {
                expiresIn: '1 hour'
            });
      
            // sending the token back to the client with a 201 status code
            res.status(201).json({ token });
        })
        .catch((error) => {
            console.log(error);

            // handling duplicate key error (e.g., username or email already in use)

            if (error.code == 11000)
                return res.status(500).json({ code: 500, msg: 'Username or email is already used' });

            // sending a generic error response if user creation fails
            res.status(500).json({ code: 500, msg: 'Unable to create user' });

        });
};

// exporting login method
export const login = async (req, res) => {
    // creating a search object to find the user by email or username
    let searcher = { email: req.body.email };
    if (!req.body.email) searcher = { username: req.body.username };

    // finding a user that matches the search criteria
    await User.findOne(searcher)
        .then(async (user) => {
            // if user is not found, throw an error
            if (!user) throw new Error('err');

            // comparing the provided password with the stored hashed password
            const passwordMatch = await user.comparePassword(req.body.password);
            // if passwords do not match, send a 401 response
            if (!passwordMatch) return res.status(401).json({ code: 401, msg: 'Wrong password' });

            // creating a JWT token for the user with their userId and a secret key
            const token = jwt.sign({ userId: user._id }, config.SECRET_KEY, {
                expiresIn: '1 hour'
            });

            // sending the token back to the client with a 201 status code
            res.status(201).json({ token, user: user.admin });
        })
        .catch((error) => {
            console.log(error);  // Logging the error to the console

            // sending a generic error response if user is not found or any other error occurs
            res.status(500).json({ code: 500, msg: 'Something went wrong' });
        });
};

