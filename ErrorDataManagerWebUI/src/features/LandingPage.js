import * as React from 'react';
import backgroundImage from '../../src/assets/backgroundImage.png';


const LandingPage = () => {

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <h2 style={{ color: 'Gray' }} >Welcome</h2>
                <img className='halfOpacity top0'
                src={backgroundImage}
            //width='600vw'
            //height='400vh'
                />
            </div>
        </>
    );
};

export default LandingPage;