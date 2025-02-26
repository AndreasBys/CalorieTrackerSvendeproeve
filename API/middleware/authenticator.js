// importing config
import config from '../config.js';
// importing jsonwebtoken library for handling JWTs
import jwt from 'jsonwebtoken';
// importing User model
import User from '../models/user.js';

// exporting authenticate middleware function
export const authenticate = async (rq, rs, next) => {
  // extracting the token from the Authorization header (assuming "Bearer <token>")
  const token = rq.headers.authorization?.split(' ')[1];

  // if no token is found, respond with a 401 status (Unauthorized)
  if (!token) {
    return rs.status(401).json({ message: 'Authentication required' });
  }

  try {
    // verifying the token using the secret key
    const decodedToken = jwt.verify(token, config.SECRET_KEY);

    // finding the user associated with the userId from the decoded token
    const user = await User.findById(decodedToken.userId);

    // if no user is found, respond with a 404 status (Not Found)
    if (!user) {
      return rs.status(404).json({ message: 'Invalid token' });
    }

    // attaching the user object to the request object for further use
    rq.user = user;

    // proceeding to the next middleware or route handler
    next();
  } catch (err) {
    // if an error occurs (e.g., token is invalid), respond with a 401 status (Unauthorized)
    rs.status(401).json({ message: 'Invalid token' });
  }
};


