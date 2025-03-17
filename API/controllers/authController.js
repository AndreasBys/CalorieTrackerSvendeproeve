// imports
import jwt from 'jsonwebtoken';
import config from '../config.js';
import User from '../models/user.js';

// register method - laver ny bruger
export const register = async (req, res) => {
    try {
        // laver ny bruger og gemmer i databasen
        const user = await new User(req.body).save();

        // laver en jwt token
        const token = jwt.sign({ userId: user._id }, config.SECRET_KEY, {
            expiresIn: '1 hour'
        });

        // returnerer token og user
        res.status(201).json({ token, user });
    } catch (error) {
        // håndterer fejl
        if (error.code == 11000) {
            return res.status(11000).json({ code: 11000, msg: 'Username or email is already used' });
        }
        res.status(500).json({ code: 500, msg: 'Unable to create user', error: error.message });
    }
};

export const login = async (req, res) => {
    // hvis request indeholder email bruges denne til at søge efter bruger, ellers bruges username
    const searcher = req.body.email ? { email: req.body.email } : { username: req.body.username };

    try {
        // finder bruger i databasen
        const user = await User.findOne(searcher);
        if (!user) throw new Error('User not found');

        // tjekker om password matcher
        const passwordMatch = await user.comparePassword(req.body.password);
        if (!passwordMatch) return res.status(401).json({ code: 401, msg: 'Wrong password' });

        // laver en jwt token
        const token = jwt.sign({ userId: user._id }, config.SECRET_KEY, {
            expiresIn: '1 hour'
        });

        // returnerer token og user
        res.status(201).json({ token, user });
    } catch (error) {
        // håndterer fejl
        res.status(500).json({ code: 500, msg: 'Something went wrong', error: error.message });
    }
};