// importing express module
import express from 'express'
import { authenticate } from '../middleware/authenticator.js';
import { createMacroTrack, getMacroTrackDay } from '../controllers/macroTrackController.js';

// creates new router from express module
const router = express.Router();

// defining routes
router.post('/', authenticate, createMacroTrack);
// get route depending if there's one or two params present
router.get('/', authenticate, (req, res, next) => {
    if (Object.keys(req.query).length = 1) {
        getMacroTrackDay(req, res, next);
    } else if (Object.keys(req.query).length = 2){
        getMacroTrackBetweenDates(req, res, next);
    }
});

// exports router as default
export default router;