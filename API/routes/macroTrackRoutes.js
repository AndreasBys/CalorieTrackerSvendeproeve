// importing express module
import express from 'express'
import { authenticate } from '../middleware/authenticator.js';
import { createMacroTrack } from '../controllers/macroTrackController.js';

// creates new router from express module
const router = express.Router();

// defining routes
router.post('/', authenticate, createMacroTrack);

// exports router as default
export default router;